using System;
using System.Collections.Generic;
using AutoMapper;
using Newtonsoft.Json;
using WebServiceCaller.Application.Notifiers.Command.CreateSmtpNotifier;
using WebServiceCaller.Application.Notifiers.Command.CreateWebService;
using WebServiceCaller.Application.Notifiers.Command.UpdateSmtpNotifier;
using WebServiceCaller.Application.Notifiers.Command.UpdateWebService;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Application.Templates.Command.CreateTemplate;
using WebServiceCaller.Application.Templates.Command.UpdateTemplate;
using WebServiceCaller.Application.Templates.ModelDto;
using WebServiceCaller.Common.Options;
using WebServiceCaller.Domain.Enum;
using WebServiceCaller.Domain.Models;

namespace WebServiceCaller.Application.Common.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Notifier

            CreateMap<Notifier, SmtpNotifierDto>().ForMember(x => x.Setting,
                opt =>
                    opt.MapFrom(des => JsonConvert.DeserializeObject<EmailSetting>(des.Setting)));

            CreateMap<Notifier, WebServiceNotifierDto>().ForMember(x => x.Setting,
                opt =>
                    opt.MapFrom(des => JsonConvert.DeserializeObject<WebServiceSetting>(des.Setting)));

            CreateMap<CreateSmtpNotifierCommand, Notifier>()
                .ForMember(x => x.ServiceType, opt => opt.MapFrom(des => ServiceType.Smtp))
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(des => DateTime.Now));

            CreateMap<CreateWebServiceCommand, Notifier>()
                .ForMember(x => x.ServiceType, opt => opt.MapFrom(des => ServiceType.WebService))
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(des => DateTime.Now));

            CreateMap<UpdateSmtpNotifierCommand, Notifier>();

            CreateMap<UpdateWebServiceCommand, Notifier>();

            CreateMap<Notifier, NotifierListDto>();

            #endregion Notifier

            #region Template

            CreateMap<Template, TemplateDto>().ForMember(x => x.Tags, opt => opt.MapFrom(des => JsonConvert.DeserializeObject<List<string>>(des.Tags)));

            CreateMap<CreateTemplateCommand, Template>()
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(des => DateTime.Now))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(des => DateTime.Now)).ForMember(x => x.Tags, opt => opt.MapFrom(des => JsonConvert.SerializeObject(des.Tags)));

            CreateMap<UpdateTemplateCommand, Template>()
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(des => DateTime.Now))
                .ForMember(x => x.Tags, opt => opt.MapFrom(des => JsonConvert.SerializeObject(des.Tags)));

            #endregion Template
        }
    }
}