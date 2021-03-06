﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.Contracts.Account;
using TradeUnionCommittee.BLL.Contracts.Dashboard;
using TradeUnionCommittee.BLL.Contracts.Directory;
using TradeUnionCommittee.BLL.Contracts.Lists.Children;
using TradeUnionCommittee.BLL.Contracts.Lists.Employee;
using TradeUnionCommittee.BLL.Contracts.Lists.Family;
using TradeUnionCommittee.BLL.Contracts.Lists.GrandChildren;
using TradeUnionCommittee.BLL.Contracts.PDF;
using TradeUnionCommittee.BLL.Contracts.Search;
using TradeUnionCommittee.BLL.Contracts.SystemAudit;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Services.Account;
using TradeUnionCommittee.BLL.Services.Dashboard;
using TradeUnionCommittee.BLL.Services.Directory;
using TradeUnionCommittee.BLL.Services.Lists.Children;
using TradeUnionCommittee.BLL.Services.Lists.Employee;
using TradeUnionCommittee.BLL.Services.Lists.Family;
using TradeUnionCommittee.BLL.Services.Lists.GrandChildren;
using TradeUnionCommittee.BLL.Services.PDF;
using TradeUnionCommittee.BLL.Services.Search;
using TradeUnionCommittee.BLL.Services.SystemAudit;
using TradeUnionCommittee.CloudStorage.Service.Extensions;
using TradeUnionCommittee.DAL.Audit.Extensions;
using TradeUnionCommittee.DAL.Extensions;
using TradeUnionCommittee.DAL.Identity.Extensions;
using TradeUnionCommittee.DataAnalysis.Service.Extensions;
using TradeUnionCommittee.PDF.Service.Extensions;

namespace TradeUnionCommittee.BLL.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddTradeUnionCommitteeServiceModule(this IServiceCollection services,
                                                                                  ConnectionStrings connectionStrings,
                                                                                  CloudStorageConnection cloudStorageConnection,
                                                                                  DataAnalysisConnection dataAnalysisConnection,
                                                                                  HashIdConfiguration setting,
                                                                                  Type autoMapperProfile = null)
        {
            // Injection => Main, Identity, Audit contexts,
            //              Cloud Storage, Data Analysis, PDF services

            
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(typeof(ConnectionProfile))));

            var cloudStorageMap = mapper.Map<CloudStorageConnection, CloudStorage.Service.Model.CloudStorageConnection>(cloudStorageConnection);
            var dataAnalysisMap = mapper.Map<DataAnalysisConnection, DataAnalysis.Service.Models.DataAnalysisConnection>(dataAnalysisConnection);

            services.AddDbContext(connectionStrings.DefaultConnection);
            services.AddIdentityContext(connectionStrings.IdentityConnection);
            services.AddAuditDbContext(connectionStrings.AuditConnection);
            services.AddCloudStorageService(cloudStorageMap, connectionStrings.CloudStorageConnection);
            services.AddDataAnalysisService(dataAnalysisMap);
            services.AddPdfService();

            if (autoMapperProfile == null)
                services.AddAutoMapper(typeof(AutoMapperProfile));
            else
                services.AddAutoMapper(autoMapperProfile, typeof(AutoMapperProfile));

            HashHelper.Initialize(setting);

            // Injection All Service
            //---------------------------------------------------------------------------------------------

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IDashboardService, DashboardService>();

            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<ISystemAuditService, SystemAuditService>();
            services.AddTransient<IPdfService, PdfService>();

            services.AddTransient<IQualificationService, QualificationService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<ISocialActivityService, SocialActivityService>();
            services.AddTransient<IPrivilegesService, PrivilegesService>();
            services.AddTransient<IAwardService, AwardService>();
            services.AddTransient<IMaterialAidService, MaterialAidService>();
            services.AddTransient<IHobbyService, HobbyService>();
            services.AddTransient<ITravelService, TravelService>();
            services.AddTransient<IWellnessService, WellnessService>();
            services.AddTransient<ITourService, TourService>();
            services.AddTransient<IActivitiesService, ActivitiesService>();
            services.AddTransient<ICulturalService, CulturalService>();
            services.AddTransient<ISubdivisionsService, SubdivisionsService>();
            services.AddTransient<IDormitoryService, DormitoryService>();
            services.AddTransient<IDepartmentalService, DepartmentalService>();

            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IPrivateHouseEmployeesService, PrivateHouseEmployeesService>();
            services.AddTransient<IPublicHouseEmployeesService, PublicHouseEmployeesService>();
            services.AddTransient<IPositionEmployeesService, PositionEmployeesService>();
            services.AddTransient<ISocialActivityEmployeesService, SocialActivityEmployeesService>();
            services.AddTransient<IPrivilegeEmployeesService, PrivilegeEmployeesService>();
            services.AddTransient<IAwardEmployeesService, AwardEmployeesService>();
            services.AddTransient<IMaterialAidEmployeesService, MaterialAidEmployeesService>();
            services.AddTransient<ITravelEmployeesService, TravelEmployeesService>();
            services.AddTransient<IWellnessEmployeesService, WellnessEmployeesService>();
            services.AddTransient<ITourEmployeesService, TourEmployeesService>();
            services.AddTransient<IActivityEmployeesService, ActivityEmployeesService>();
            services.AddTransient<ICulturalEmployeesService, CulturalEmployeesService>();
            services.AddTransient<IGiftEmployeesService, GiftEmployeesService>();
            services.AddTransient<IFluorographyEmployeesService, FluorographyEmployeesService>();
            services.AddTransient<IApartmentAccountingEmployeesService, ApartmentAccountingEmployeesService>();
            services.AddTransient<IHobbyEmployeesService, HobbyEmployeesService>();

            services.AddTransient<IFamilyService, FamilyService>();
            services.AddTransient<ITravelFamilyService, TravelFamilyService>();
            services.AddTransient<IWellnessFamilyService, WellnessFamilyService>();
            services.AddTransient<ITourFamilyService, TourFamilyService>();
            services.AddTransient<IActivityFamilyService, ActivityFamilyService>();
            services.AddTransient<ICulturalFamilyService, CulturalFamilyService>();

            services.AddTransient<IChildrenService, ChildrenService>();
            services.AddTransient<IHobbyChildrenService, HobbyChildrenService>();
            services.AddTransient<ITravelChildrenService, TravelChildrenService>();
            services.AddTransient<IWellnessChildrenService, WellnessChildrenService>();
            services.AddTransient<ITourChildrenService, TourChildrenService>();
            services.AddTransient<IActivityChildrenService, ActivityChildrenService>();
            services.AddTransient<ICulturalChildrenService, CulturalChildrenService>();
            services.AddTransient<IGiftChildrenService, GiftChildrenService>();

            services.AddTransient<IGrandChildrenService, GrandChildrenService>();
            services.AddTransient<IHobbyGrandChildrenService, HobbyGrandChildrenService>();
            services.AddTransient<ITravelGrandChildrenService, TravelGrandChildrenService>();
            services.AddTransient<ITourGrandChildrenService, TourGrandChildrenService>();
            services.AddTransient<IActivityGrandChildrenService, ActivityGrandChildrenService>();
            services.AddTransient<ICulturalGrandChildrenService, CulturalGrandChildrenService>();
            services.AddTransient<IGiftGrandChildrenService, GiftGrandChildrenService>();

            //---------------------------------------------------------------------------------------------

            return services;
        }

        private class ConnectionProfile : Profile
        {
            public ConnectionProfile()
            {
                CreateMap<CloudStorageConnection, CloudStorage.Service.Model.CloudStorageConnection>();
                CreateMap<DataAnalysisConnection, DataAnalysis.Service.Models.DataAnalysisConnection>();
            }
        }
    }
}