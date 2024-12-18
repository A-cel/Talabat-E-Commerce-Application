﻿using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using System.Linq.Expressions;
using System.Reflection;
using Talabat.Api.Dtos;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Api.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return $"{configuration["ApiBaseUrl"]}/{source.Product.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
