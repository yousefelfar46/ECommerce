using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOS.Orders;
using ECommerce.Application.Specifications;
using ECommerce.Application.Specifications.OrderModule;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Orders;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IBasketRepository _basketRepository;   
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork uow, IBasketRepository basketRepository, IMapper mapper)
        {
            _uow = uow;
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email, CancellationToken ct = default)
        {
            //Items (Order Item)
            //Ship TO Address (Order Address)

            var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId);

            if (basket == null) return Error.NotFound("Basket Not Found ", $"Basket With This Id {orderDto.BasketId} Is Not Found");
            if (basket.Items.Count == 0) return Error.Validation("Basket Is Empty ", $"Can not Create Order WIth Basket {orderDto.BasketId}");



            ArgumentNullException.ThrowIfNull(basket.PaymentIntentId);


            var _orderRepo = _uow.GetRepository<Order, Guid>();

            var existingOrder = await _orderRepo.GetByIdAsync(new OrderWithPaymentIntentSpecifications(basket.PaymentIntentId));
            if (existingOrder is not null) _orderRepo.Remove(existingOrder);


            var productIds = basket.Items.Select(b => b.Id).ToHashSet();

            var products = (await _uow.GetRepository<Product, int>()
                              .GetAllAsync(new ProductsWithIdSpecifications(productIds))).ToDictionary(x => x.Id);


            var orderItems = new List<OrderItem>(basket.Items.Count);
            foreach (var basketItem in basket.Items)
            {

                //get the original product Of basketItem from the products List  {Specifications}


                products.TryGetValue(basketItem.Id, out var originalProduct);

                orderItems.Add(new OrderItem()
                {
                    Quantity = basketItem.Quantity,
                    Price = originalProduct.Price,
                    Product = new ProductItemOrdered()
                    {
                        PictureUrl = originalProduct.PictureUrl,
                        ProductName = originalProduct.Name,
                        ProductId = originalProduct.Id,
                    }
                });

            }


            var orderAddress = _mapper.Map<OrderAddress>(orderDto.ShipToAddress);

            var deliveryMethod = await _uow.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId);

            if (deliveryMethod == null)
                return Error.NotFound("Delivery Method Not Found", $"Delivery Method With Id {orderDto.DeliveryMethodId} Is Not Found");

            var subTotal = orderItems.Sum(x => x.Quantity * x.Price);




            var order = new Order(email, orderAddress, orderItems, deliveryMethod.Id, subTotal, basket.PaymentIntentId);
            _orderRepo.Add(order);
            var result = await _uow.SaveChangesAsync();
            if (result == 0)
            {
                return Error.Failure("Order Save Failed ", " Cannot Create Order");
            }
            else
            {
                await _basketRepository.DeleteBasketAsync(orderDto.BasketId);
                return _mapper.Map<OrderToReturnDto>(order);
            }

        }

        public async Task<Result<IReadOnlyList<OrderToReturnDto>>> GetAllOrdersForUserAsync(string email, CancellationToken ct = default)
        {
            var orders = await _uow.GetRepository<Order, Guid>().GetAllAsync(new OrderSpecifications(email), ct);
            if (orders.Any())
            {
                return Result<IReadOnlyList<OrderToReturnDto>>.Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
            }
            else return Error.NotFound("Orders Not Found", $"No Orders Found For : {email}");
        }



        public async Task<Result<OrderToReturnDto>> GetOrderByIdAndEmailForUserAsync(Guid id, string email, CancellationToken ct = default)
        {
            var order = await _uow.GetRepository<Order, Guid>().GetByIdAsync(new OrderSpecifications(id, email), ct);
            if (order != null)
            {
                return Result<OrderToReturnDto>.Ok(_mapper.Map<OrderToReturnDto>(order));
            }
            else return Error.NotFound("Order Not Found", $" Order With Id {id} Is Not Found For : {email}");
        }


        public async Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethodAsync(CancellationToken ct = default)
        {
            var deliveryMethods = await _uow.GetRepository<DeliveryMethod, int>().GetAllAsync(ct);
            if (deliveryMethods.Any())
                return Result<IReadOnlyList<DeliveryMethodDto>>
                    .Ok(_mapper.Map<IReadOnlyList<DeliveryMethodDto>>(deliveryMethods));

            else
                return Error.NotFound("No Delivery Methods Found");

        }
    }
}
