using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyChef.Backend.Rest.Models;
using EasyChef.Contracts.Shared.Models;

namespace EasyChef.Backend.Rest
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMapBothWays<CategoryDTO, Category>();
            CreateMapBothWays<ProductDTO, Product>();
            CreateMapBothWays<ShoppingCartDTO, ShoppingCart>();
            CreateMapBothWays<ShoppingCartProductDTO, ShoppingCartProduct>();
            CreateMapBothWays<UserDTO, User>();
            CreateMapBothWays<RecepyDTO, Recepy>();
            CreateMapBothWays<IngredientDTO, Ingredient>();
            CreateMapBothWays<RecepyPreparationDTO, RecepyPreparation>();
        }

        private void CreateMapBothWays<TIn, TOut>()
        {
            CreateMap<TIn, TOut>();
            CreateMap<TOut, TIn>();
        }
    }
}
