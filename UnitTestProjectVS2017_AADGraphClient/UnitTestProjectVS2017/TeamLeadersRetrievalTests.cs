using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
//using AdminPortal.BusinessServices;
//using AdminPortal.BusinessServices.GraphApiHelper;
//using AdminPortal.UnitTests.Common;
//using AdminPortal.UnitTests.TestUtilities;
 using FluentAssertions;
//using Microsoft.Extensions.Configuration;

namespace UnitTestProjectCore
{
    [TestClass]
    public class TeamLeadersRetrievalTests
    {

        [TestMethod]
        [TestCategory("GraphClient")]
        public void TeamLeadersRetrieval_ServiceCenterManagerRoleId_ReturnsEmailList()
        {
            //Arrange
            var activeDirectoryClient = GetGraphClientSubstituteWithGroupMembers();
            //Assert
             activeDirectoryClient.Should().NotBeNull();
            //emailList.Should().NotBeNull();
            //emailList.Count().Should().Be(1);
            //emailList.Should().Contain("scm.test@test");
        }



        private IActiveDirectoryClient GetGraphClientSubstituteWithNullGroupMembers()
        {
            var graphClient = Substitute.For<IActiveDirectoryClient>();
            var pagedCollection = GetApplicationPagedCollection();
            var groupSubstitute = GetGroupSubstitute();

            var applicationCollection = Substitute.For<IApplicationCollection>();
            applicationCollection.ExecuteAsync().ReturnsForAnyArgs(pagedCollection);

            graphClient.Applications.ReturnsForAnyArgs(applicationCollection);

            var groupFetcher = Substitute.For<IGroupFetcher>();
            graphClient.Groups[Arg.Any<string>()].Returns(groupFetcher);
            groupFetcher.ExecuteAsync().ReturnsForAnyArgs((IGroup)groupSubstitute);

            return graphClient;

        }

        private IActiveDirectoryClient GetGraphClientSubstituteWithGroupMembers()
        {

            var graphClient = Substitute.For<IActiveDirectoryClient>();
            var pagedCollection = GetApplicationPagedCollection();
            var groupSubstitute = GetGroupWithMembersSubstitute();
            var directoryObjectPageCollection = GetDirectoryObjectPageCollection();

            var applicationCollection = Substitute.For<IApplicationCollection>();

            applicationCollection.ExecuteAsync().ReturnsForAnyArgs(pagedCollection);

            graphClient.Applications.ReturnsForAnyArgs(applicationCollection);

            var groupFetcher = Substitute.For<IGroupFetcher>();
            graphClient.Groups[Arg.Any<string>()].Returns(groupFetcher);
            groupFetcher.ExecuteAsync().ReturnsForAnyArgs((IGroup)groupSubstitute);

            var directoryObjectCollection = Substitute.For<IDirectoryObjectCollection>();
            groupFetcher.Members.Returns(directoryObjectCollection);//
            directoryObjectCollection.ExecuteAsync().Returns(directoryObjectPageCollection);


            // FOR DEBUGGING
            //var applicationsFromAwait =AsyncSubstituteClient(graphClient);

            //var graphClientApplications = graphClient.Applications;
            //IPagedCollection<IApplication> appCollection = graphClientApplications.ExecuteAsync().Result;
            //var morePagesAvailable = false;
            //morePagesAvailable = appCollection.MorePagesAvailable;
            //List<IApplication> applications = appCollection.CurrentPage.ToList();


            // var applicationFetcher = GetApplicationFetcher();
            //graphClient.Applications[Arg.Any<string>()].ReturnsForAnyArgs(applicationFetcher);

            return graphClient;

        }

        private IGroupFetcher GetGroupSubstitute()
        {
            var group = Substitute.For<IGroupFetcher, IGroup>();
            IPagedCollection<IDirectoryObject> groupMembers = Substitute.For<IPagedCollection<IDirectoryObject>>();
            var directoryObjectCollection = Substitute.For<IDirectoryObjectCollection>();
            group.Members.Returns(directoryObjectCollection);
            directoryObjectCollection.ExecuteAsync().Returns(groupMembers);
            return group;
        }
        private IGroupFetcher GetGroupWithMembersSubstitute()
        {
            var group = Substitute.For<IGroupFetcher, IGroup>();
            var pagedCollection = Substitute.For<IPagedCollection<IDirectoryObject>>();
            
            var currentPage = GetDirectoryObjectList();
            pagedCollection.CurrentPage.ReturnsForAnyArgs(currentPage);
            var groupMembers = Substitute.For<IDirectoryObjectCollection>();
            group.Members.Returns(groupMembers);
            groupMembers.ExecuteAsync().Returns(pagedCollection);
            return group;
        }

        private IReadOnlyList<IDirectoryObject> GetDirectoryObjectList()
        {
            var user = Substitute.For<IDirectoryObject,IUser,IUserFetcher >();

            var appRoleAssignmentList = new List<IAppRoleAssignment>() { GetAppRoleAssignment() };

            var appRolesAssignmentsPagedCollection = Substitute.For<IPagedCollection<IAppRoleAssignment>>();
            appRolesAssignmentsPagedCollection.CurrentPage.Returns(appRoleAssignmentList);
            var appRoleAssignmentCollection = Substitute.For<IAppRoleAssignmentCollection>();
            ((IUserFetcher)user).AppRoleAssignments.Returns(appRoleAssignmentCollection);
            appRoleAssignmentCollection.ExecuteAsync().Returns(appRolesAssignmentsPagedCollection);

            ((IUser)user).AppRoleAssignments.Returns(appRolesAssignmentsPagedCollection);
            ((IUser)user).Mail.Returns("scm.test@test");
          

            IReadOnlyList<IDirectoryObject> directoryObjectList = new List<IDirectoryObject> { user };
          
            return directoryObjectList;
        }
        
        private IPagedCollection<IDirectoryObject> GetDirectoryObjectPageCollection()
        {
            var directoryObjectReadOnlyList = GetDirectoryObjectReadOnlyList();
            var directoryObjectPageCollection = Substitute.For<IPagedCollection<IDirectoryObject>>();
            directoryObjectPageCollection.CurrentPage.Returns(directoryObjectReadOnlyList);
            return directoryObjectPageCollection;
        }

        private DirectoryObject GetDirectoryObject()
        {
            var memberDirectoryObject = new DirectoryObject
            {
                ObjectType = "User",
                ObjectId = ""

            };
            return memberDirectoryObject;

        }

        private IActiveDirectoryClient GetGraphClientSubstituteWithNullServiceCenterManagerRoleId()
        {
            var graphClient = Substitute.For<IActiveDirectoryClient>();

            IPagedCollection<IApplication> pagedCollection = Substitute.For<IPagedCollection<IApplication>>();
            IReadOnlyList<IApplication> currentPageReadOnlyList = new List<IApplication> { new Application() { AppId = "23442f66-fe21c-4d89-a5ca-8a8ebc2we987" } };
            pagedCollection.CurrentPage.ReturnsForAnyArgs(currentPageReadOnlyList);

            var applicationCollection = Substitute.For<IApplicationCollection>();
            applicationCollection.ExecuteAsync().ReturnsForAnyArgs(pagedCollection);

            graphClient.Applications.ReturnsForAnyArgs(applicationCollection);



            return graphClient;

        }

        private IActiveDirectoryClient GetSubstituteForActiveDirectoryClient()
        {
            var graphClient = Substitute.For<IActiveDirectoryClient>();
            var pagedCollection = GetApplicationPagedCollection();
            var groupSubstitute = GetGroupSubstitute();
         //   var directoryObjectPageCollection = GetDirectoryObjectPageCollection();

            var applicationCollection = Substitute.For<IApplicationCollection>();
            applicationCollection.ExecuteAsync().ReturnsForAnyArgs(pagedCollection);//.ToTask()

            graphClient.Applications.ReturnsForAnyArgs(applicationCollection);

            var groupFetcher = Substitute.For<IGroupFetcher>();
            graphClient.Groups[Arg.Any<string>()].Returns(groupFetcher);
            groupFetcher.ExecuteAsync().ReturnsForAnyArgs((IGroup)groupSubstitute);

            //graphClient.Groups[Arg.Any<string>()].ReturnsForAnyArgs((IGroupFetcher)groupSubstitute);//(realGroupFetcherObj);

            // graphClient.Groups[Arg.Any<string>()].Members.ExecuteAsync().Returns(directoryObjectPageCollection);


       //     var result = graphClient.Groups[Arg.Any<string>()].ExecuteAsync().Result;
       //     var fetcher = (IGroupFetcher)result;

            // FOR DEBUGGING
            //var applicationsFromAwait =AsyncSubstituteClient(graphClient);

            //var graphClientApplications = graphClient.Applications;
            //IPagedCollection<IApplication> appCollection = graphClientApplications.ExecuteAsync().Result;
            //var morePagesAvailable = false;
            //morePagesAvailable = appCollection.MorePagesAvailable;
            //List<IApplication> applications = appCollection.CurrentPage.ToList();


            // var applicationFetcher = GetApplicationFetcher();
            //graphClient.Applications[Arg.Any<string>()].ReturnsForAnyArgs(applicationFetcher);

            return graphClient;

        }
        

        
        private IPagedCollection<IApplication> GetApplicationPagedCollection()
        {
            IPagedCollection<IApplication> pagedCollection = Substitute.For<IPagedCollection<IApplication>>();

            IReadOnlyList<IApplication> currentPageReadOnlyList = GetApplicationReadOnlyList();

            pagedCollection.CurrentPage.ReturnsForAnyArgs(currentPageReadOnlyList);
            pagedCollection.MorePagesAvailable.ReturnsForAnyArgs(true);
            return pagedCollection;
        }
        
        private IAppRoleAssignment GetAppRoleAssignment()
        {
            var appRoleAssignment = Substitute.For<IAppRoleAssignment>();
            appRoleAssignment.Id = Guid.Parse("1579eed6-fcb5-4448-b8ec-e62cb4d46f59"); //ServiceCenterManager Role Id

            return appRoleAssignment;

        }
        
        private IReadOnlyList<IApplication> GetApplicationReadOnlyList()
        {
            var appRoles = GetAppRoles();
            var application = GetApplication(appRoles);
            IReadOnlyList<IApplication> applicationList = new List<IApplication> { application };

            return applicationList;
        }
        
        private static IList<AppRole> GetAppRoles()
        {
            IList<AppRole> appRoles = new List<AppRole>();
            AppRole appRole = new AppRole();
            appRole.Id = Guid.Parse("1579eed6-fcb5-4448-b8ec-e62cb4d46f59");
            appRole.Description = "Service Center Manager";
            appRole.DisplayName = "ServiceCenterManager";
            appRole.Value = "ServiceCenterManager";
            appRoles.Add(appRole);
            return appRoles;
        }

        private static IApplication GetApplication(IList<AppRole> appRoles)
        {
            var application = Substitute.For<IApplication>();//new Application(); //unable to use Substitute.For<IApplication>() has AppRoles read-only
            string appid = "43c42f66-e21c-4d89-a5ca-8a8ebc2be260";
            application.AppId = appid;
            application.AppRoles.Returns(appRoles);
            return application;
        }
   
        private IReadOnlyList<DirectoryObject> GetDirectoryObjectReadOnlyList()
        {
            var dirObject = GetDirectoryObject();
            IReadOnlyList<DirectoryObject> directoryObjectList = new List<DirectoryObject> { dirObject };

            return directoryObjectList;
        }
    }
}
