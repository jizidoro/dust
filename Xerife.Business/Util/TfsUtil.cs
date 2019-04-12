using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using Xerife.Entities.Util;
using System.Linq;
using Xerife.Entities;
using System.Configuration;
using System.Collections.Generic;

namespace Xerife.Business.Util
{

    public static class TfsUtil
    {
        //private static string _personalAccessToken = "vixintegracao:vixteam*2015";
        private static string _tfsUser = "vixintegracao";// ConfigurationManager.AppSettings["IntegrationUser"];
        private static string _tfsPassword = "vixteam*2015";// ConfigurationManager.AppSettings["IntegrationUserPassword"];

        //Get
        private static string _collectionsGetUri = "_apis/projectCollections/?api-version=2.2";
        private static string _projectsGetUri = "{0}/_apis/projects?  api-version=2.2";
        private static string _teamsGetUri = "{0}/_apis/projects/{1}/teams?api-version=2.2";
        private static string _teamIterationsGetUri = "{0}/{1}/{2}/_apis/work/TeamSettings/Iterations?api-version=v2.0-preview";
        private static string _teamCapacity = "{0}/{1}/{2}/_apis/work/TeamSettings/Iterations/{3}/Capacities?api-version=2.0-preview.1";
        private static string _tfsRootFolder = "{0}/_apis/tfvc/items?scopePath=$/{1}&api-version=1.0-preview.1";
        private static string _changesetByDatetUri = "{0}/{1}/_apis/tfvc/changesets?fromDate={2}&toDate={3}&api-version=1.1";
        private static string _changesetByPathUri = "{0}/{1}/_apis/tfvc/changesets?searchCriteria.itemPath={2}&api-version=1.0";
        private static string _branchRootUri = "{0}/_apis/tfvc/branches?api-version=1.0-preview.1";
        private static string _areaPathTree = "{0}/{1}/_apis/wit/classificationNodes/areas?$depth=5&api-version=1.0";
        private static string _workItemApontamento = "{0}/_apis/wit/workitems/{1}?api-version=1.0";
        private static string _workItemUpdateUri = "{0}/_apis/wit/workitems/{1}?api-version=2.2";
        private static string _changesetWorkItems = "{0}/_apis/tfvc/changesets/{1}/workItems?api-version=3.1";
        private static string _branchFromPath = "{0}/_apis/tfvc/branches?scopePath={1}&includeChildren=true&api-version=3.1";
        //Post
        private static string _workItemQueryGetUri = "{0}/{1}/_apis/wit/wiql?api-version=1.0";


        private static object TfsClient(string addressUri, string requestUri, Type tipoEntidade)
        {
            //string _credentials = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(_personalAccessToken));
            HttpClientHandler authtHandler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential(_tfsUser, _tfsPassword)// CredentialCache.DefaultNetworkCredentials
            };
            //use the httpclient        
            using (var client = new HttpClient(authtHandler))
            {
                //set our headers
                client.BaseAddress = new Uri(addressUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //send the request and content
                HttpResponseMessage response = client.GetAsync(requestUri).Result;

                if (response.IsSuccessStatusCode)
                {
                    var temp2 = response.Content.ReadAsStringAsync();
                    var temp = response.Content.ReadAsAsync(tipoEntidade);
                    //var json = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                    return temp.Result;
                }
                else
                {
                    return null;
                }

            }
        }

        private static T TfsClientGenericGet<T>(string addressUri, string requestUri)
        {
            //string _credentials = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(_personalAccessToken));
            HttpClientHandler authtHandler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential(_tfsUser, _tfsPassword)// CredentialCache.DefaultNetworkCredentials
            };
            //use the httpclient        
            using (var client = new HttpClient(authtHandler))
            {
                //set our headers
                client.BaseAddress = new Uri(addressUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //send the request and content
                HttpResponseMessage response = client.GetAsync(requestUri).Result;

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return default(T);
                }

            }
        }

        private static T TfsClientGenericPost<T>(string addressUri, string requestUri, dynamic postJsonContent = null)
        {
            //string _credentials = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(_personalAccessToken));
            HttpClientHandler authtHandler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential(_tfsUser, _tfsPassword)// CredentialCache.DefaultNetworkCredentials
            };
            //use the httpclient        
            using (var client = new HttpClient(authtHandler))
            {
                //set our headers
                client.BaseAddress = new Uri(addressUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(postJsonContent);
                var jsonString = json.ToString();
                var strContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                //send the request and content
                HttpResponseMessage response = client.PostAsync(requestUri, strContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<T>().Result;
                }
                else
                {
                    return default(T);
                }

            }
        }

        public static TfsCollectionsListViewModel GetProjectCollection(string tfsBaseAddress)
        {
            return TfsClientGenericGet<TfsCollectionsListViewModel>(tfsBaseAddress, _collectionsGetUri);
        }

        public static TfsProjectListViewModel GetProjects(string tfsBaseAddress, string collection)
        {
            return TfsClientGenericGet<TfsProjectListViewModel>(tfsBaseAddress, string.Format(_projectsGetUri, collection));
        }

        public static TfsTeamListViewModel GetTeams(string tfsBaseAddress, string collection, string project)
        {
            return TfsClientGenericGet<TfsTeamListViewModel>(tfsBaseAddress, string.Format(_teamsGetUri, collection, project));
        }

        public static TfsTeamIterationListViewModel GetTeamIterations(string tfsBaseAddress, string collection, string project, string team)
        {
            return TfsClientGenericGet<TfsTeamIterationListViewModel>(tfsBaseAddress, string.Format(_teamIterationsGetUri, collection, project, team));
        }

        public static WIQLViewModel GetWIQLbyIteration(string tfsBaseAddress, string collection, string project, string iteration)
        {
            var content = new
            {
                query = "SELECT [System.Id] FROM WorkItems WHERE [System.IterationPath] = '" + iteration + "' AND [System.State] = 'Resolved'"
            };
            return GetWIQL(tfsBaseAddress, string.Format(_workItemQueryGetUri, collection, project), content);
        }

        public static WorkItem GetWorkItem(string tfsBaseAddress, string collection, int idWorkItem)
        {
            return TfsClientGenericGet<WorkItem>(tfsBaseAddress, string.Format(_workItemApontamento, collection, idWorkItem));
        }

        public static WIQLViewModel GetIssuesFromProject(string tfsBaseAddress, string collection, string project, string iteration)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [System.Id], [System.CreatedDate], [Microsoft.VSTS.CMMI.TargetResolveDate] ");
            sb.Append("FROM WorkItems ");
            sb.Append("WHERE ");
            sb.Append("[System.IterationPath] = '" + iteration + "' ");
            sb.Append("AND [System.WorkItemType] = 'Issue' ");
            sb.Append("AND [Microsoft.VSTS.CMMI.Escalate]='Yes' ");
            var content = new
            {
                query = sb.ToString()
            };
            return GetWIQL(tfsBaseAddress, string.Format(_workItemQueryGetUri, collection, project), content);
        }

        public static WIQLViewModel GetWorkItemFromIteration(string tfsBaseAddress, string collection, string project, string iteration)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [System.Id] ");
            sb.Append("FROM WorkItems ");
            sb.Append("WHERE ");
            sb.Append("[System.IterationPath] = '" + iteration + "' ");
            sb.Append("AND ([System.WorkItemType] = 'Task' OR [System.WorkItemType] = 'Bug') ");
            sb.Append("AND [Custom.Timesheets.TimesheetRawData] CONTAINS 'false' ");
            var content = new
            {
                query = sb.ToString()
            };
            return GetWIQL(tfsBaseAddress, string.Format(_workItemQueryGetUri, collection, project), content);
        }

        public static List<WorkItem> IterationWorkItemsUnclosed(string tfsBaseAddress, string collection, string project, string iteration)
        {
            var content = new
            {
                query = "SELECT [System.Id] FROM WorkItems WHERE [System.IterationPath] = '" + iteration + "' AND [System.State] <> 'Closed' AND ([System.WorkItemType] ='Task' OR [System.WorkItemType] ='Bug')"
            };
            var result = GetWIQL(tfsBaseAddress, string.Format(_workItemQueryGetUri, collection, project), content);
            if (result == null)
                return null;

            return result.workItems;
        }

        private static WIQLViewModel GetWIQL(string tfsBaseAddress, string requestUri, dynamic content)
        {
            return TfsClientGenericPost<WIQLViewModel>(tfsBaseAddress, requestUri, content);
        }

        public static TfsTeamCapacityListViewModel GetTeamCapacity(string tfsBaseAddress, string collection, string project, string team, string iteration)
        {
            return TfsClientGenericGet<TfsTeamCapacityListViewModel>(tfsBaseAddress, string.Format(_teamCapacity, collection, project, team, iteration));
        }

        public static List<ChangeSetViewModel> GetChangesetProjectIteration(string tfsBaseAddress, string collection, string project, string team, string iteration, string path)
        {
            var currentIteration = GetIterationDetail(tfsBaseAddress, collection, project, team, iteration);
            var changesetDateList = TfsClientGenericGet<ChangesetListViewModel>(tfsBaseAddress, string.Format(_changesetByDatetUri, collection, project, Convert.ToDateTime(currentIteration.attributes.startDate).ToString("dd/MM/yyyy"), Convert.ToDateTime(currentIteration.attributes.finishDate).ToString("dd/MM/yyyy")));
            var changesetyPathList = TfsClientGenericGet<ChangesetListViewModel>(tfsBaseAddress, string.Format(_changesetByPathUri, collection, project, path));

            if (changesetyPathList == null || changesetDateList == null)
                return new List<ChangeSetViewModel>();

            return (from cp in changesetDateList.value
                    join cd in changesetyPathList.value
                                           on cp.changesetId equals cd.changesetId
                    select new ChangeSetViewModel
                    {
                        author = cp.author,
                        changesetId = cp.changesetId,
                        checkedInBy = cp.checkedInBy,
                        comment = cp.comment,
                        createdDate = cp.createdDate,
                        url = cp.url
                    }).ToList();
        }

        public static List<ChangeSetViewModel> ProjetoIterationChangesets(string tfsBaseAddress, string collection, string project, string team, string iteration, string path)
        {
            return GetChangesetProjectIteration(tfsBaseAddress, collection, project, team, iteration, path);
        }

        public static List<TfsChangesetWorkItems> GetChangesetWorkItems(string tfsBaseAddress, string collection, List<ChangeSetViewModel> changesets)
        {
            List<TfsChangesetWorkItems> changesetList = new List<TfsChangesetWorkItems>();
            foreach (ChangeSetViewModel changeset in changesets)
            {
                changesetList.Add(TfsClientGenericGet<TfsChangesetWorkItems>(tfsBaseAddress, string.Format(_changesetWorkItems, collection, changeset.changesetId)));
            }
            return changesetList.Where(x => x.count > 0).ToList();
        }

        public static TfsTeamCapacityListViewModel GetCapacityFromCurrentIteration(Projeto p)
        {
            //var iterations = GetTeamIterations(p.TfsUrl, p.TfsCollection, p.TfsProject, p.TfsTeam);
            //var currentIteration = iterations.value.Where(x => DateTime.Now >= (DateTime)x.attributes.startDate && DateTime.Now <= (DateTime)x.attributes.finishDate).FirstOrDefault();
            var currentIteration = GetCurrentIteration(p);
            if (currentIteration != null)
                return GetTeamCapacity(p.TfsUrl, p.TfsCollection, p.TfsProject, p.TfsTeam, currentIteration.id);
            return new TfsTeamCapacityListViewModel();
        }

        public static TfsTeamIterationViewModel GetCurrentIteration(Projeto p)
        {
            return GetTeamIterations(p.TfsUrl, p.TfsCollection, p.TfsProject, p.TfsTeam).value.Where(x => DateTime.Now >= (DateTime)x.attributes.startDate && DateTime.Now <= (DateTime)x.attributes.finishDate).FirstOrDefault();
        }

        public static TfsTeamIterationViewModel GetIterationDetail(string tfsBaseAdrress, string collection, string project, string team, string iteration)
        {
            return GetTeamIterations(tfsBaseAdrress, collection, project, team).value.Where(x => x.name.Equals(iteration.Split('\\').LastOrDefault())).FirstOrDefault();
        }

        public static List<TfsTeamIterationViewModel> GetProjectIterations(Projeto p)
        {
            return GetTeamIterations(p.TfsUrl, p.TfsCollection, p.TfsProject, p.TfsTeam).value.ToList();
        }

        public static TfsRootFolderListViewModel GetTfsFolderFromPath(string tfsBaseAddress, string collection, string folderPath)
        {
            return TfsClientGenericGet<TfsRootFolderListViewModel>(tfsBaseAddress, string.Format(_tfsRootFolder, collection, folderPath));
        }

        public static BranchListViewModel GetBranchByRoot(string tfsBaseAddress, string collection)
        {
            return TfsClientGenericGet<BranchListViewModel>(tfsBaseAddress, string.Format(_branchRootUri, collection));
        }

        public static BranchListViewModel GetBranchByPath(string tfsBaseAddress, string collection, string path)
        {
            return TfsClientGenericGet<BranchListViewModel>(tfsBaseAddress, string.Format(_branchFromPath, collection, path));
        }

        public static AreaPathTreeViewModel GetAreaPathTree(string tfsBaseAddress, string collection, string project)
        {
            var lista = TfsClientGenericGet<AreaPathTreeViewModel>(tfsBaseAddress, string.Format(_areaPathTree, collection, project));
            return TfsClientGenericGet<AreaPathTreeViewModel>(tfsBaseAddress, string.Format(_areaPathTree, collection, project));
        }

        public static bool UpdateWorkItem(string tfsBaseAddress, string collection, WorkItemFieldsUpdate[] workItemFields, int idWorkItem)
        {
            Object[] patchDocument = new Object[workItemFields.Length];
            for (int i = 0; i < workItemFields.Length; i++)
            {
                patchDocument[i] = new
                {
                    op = workItemFields[i].op,
                    path = workItemFields[i].path,
                    value = workItemFields[i].value
                };
            }

            //string _credentials = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(_personalAccessToken));
            HttpClientHandler authtHandler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential(_tfsUser, _tfsPassword)// CredentialCache.DefaultNetworkCredentials
            };
            //use the httpclient        
            using (var client = new HttpClient(authtHandler))
            {
                client.BaseAddress = new Uri(tfsBaseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //serialize the fields array into a json string
                var patchValue = new StringContent(JsonConvert.SerializeObject(patchDocument), Encoding.UTF8, "application/json-patch+json");

                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, string.Format(_workItemUpdateUri, collection, idWorkItem)) { Content = patchValue };
                var response = client.SendAsync(request).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                return response.IsSuccessStatusCode;
            }
        }

        //VIDA NOVA
        //
        public static TfsRootFolderListViewModel GetTfsProjectPath(string tfsBaseAddress, string collection, string folderPath)
        {
            return TfsClientGenericGet<TfsRootFolderListViewModel>(tfsBaseAddress, string.Format(_tfsRootFolder, collection, folderPath));
        }

    }
}