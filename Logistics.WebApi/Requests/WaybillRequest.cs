using System;
using FluentValidation;
using Logistics.WebApi.Helpers;

namespace Logistics.WebApi.Requests
{
    public record InvoiceRequest(
           string ShipperName,
           string ShipperPhone,
           string? IdCard,
           int ToStation,
           int TransitStation,
           string Address,
           string ConsigneeName,
           string ConsigneePhone,
           string CargoName,
           string? CargoId,
           int Quantity,
           float Volume,
           float Weight,
           int Packing,
           int CargoValue,
           decimal InsuranceFee,
           decimal FreightFee,
           decimal CodcFee,
           decimal BackFreightFee,
           decimal DeliveryFee,
           decimal NotificationFee,
           string? Remarks,
           int BankId,
           Guid BargainerId);

    public class WaybillRequest
    {
        public class InvoiceRequestValidator : AbstractValidator<InvoiceRequest>
        {
            public InvoiceRequestValidator()
            {
                RuleFor(e => e.ShipperName).NotNull().NotEmpty().WithMessage("发货人不能为空");
                RuleFor(e => e.ConsigneeName).NotNull().NotEmpty().WithMessage("收货人不能为空");
                RuleFor(e => e.Address).NotNull().NotEmpty().WithMessage("送货地址不能为空");
                RuleFor(e => e.ShipperPhone).NotNull().NotEmpty().WithMessage("手机号不能为空").Must(Helper.IsValidPhoneNumber).WithMessage("非法手机号");
                RuleFor(e => e.ConsigneePhone).NotNull().NotEmpty().WithMessage("手机号不能为空").Must(Helper.IsValidPhoneNumber).WithMessage("非法手机号");
                RuleFor(e => e.FreightFee).NotNull().NotEmpty().WithMessage("运费不能为空")
                    .GreaterThan(0).WithMessage("运费必须大于0");
            }
        }
    }
}

