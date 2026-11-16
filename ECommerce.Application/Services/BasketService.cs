using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOS.Baskets;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Baskets;

namespace ECommerce.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, TimeSpan? ttl)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basket);
            var basketRes = await _basketRepository.CreateOrUpdateBasketAsync(customerBasket, ttl);
            return basketRes == null ? Result<BasketDto>.Fail(Error.Failure("Basket Creation Fail", " Cannot Create Or Update Basket")) :
                Result<BasketDto>.Ok(basket);
        }

        public async Task<Result<bool>> DeleteBasketAsync(string basketId)
        {
            var result = await _basketRepository.DeleteBasketAsync(basketId);
            return result ? Result<bool>.Ok(true) : Result<bool>.Fail(Error.Failure("Basket Delete Failute", "Cannot Delete Basket "));
        }

        public async Task<Result<BasketDto>> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            return basket == null ? Result<BasketDto>.Fail(Error.NotFound("Basket Not Fount")) :
                _mapper.Map<BasketDto>(basket);
        }
    }
}
