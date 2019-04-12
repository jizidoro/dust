using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xerife.Entities.Util
{

    public class TfsCollectionsListViewModel
    {
        public int count { get; set; }
        public TfsCollectionViewModel[] value { get; set; }
    }
    public class TfsCollectionViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string state { get; set; }
    }
    public class TfsProjectListViewModel
    {
        public int count { get; set; }
        public TfsProjectViewModel[] value { get; set; }
    }
    public class TfsProjectViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string state { get; set; }
    }
    public class TfsTeamIterationListViewModel
    {
        public int count { get; set; }
        public TfsTeamIterationViewModel[] value { get; set; }
    }
    public class TfsTeamIterationViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public TfsTeamIterationDatesViewModel attributes { get; set; }
        public string url { get; set; }
    }
    public class TfsTeamIterationDatesViewModel
    {
        public object startDate { get; set; }
        public object finishDate { get; set; }
    }
    #region Team
    public class TfsTeamListViewModel
    {
        public TfsTeamViewModel[] value { get; set; }
        public int count { get; set; }
    }
    public class TfsTeamViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string identityUrl { get; set; }
    }
    #endregion
    #region WorkItemQueryLanguage
    public class Column
    {
        public string referenceName { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class WorkItem
    {
        public int id { get; set; }
        public string url { get; set; }
        public WorkItemFields fields { get; set; }
    }


    public class WorkItemFields
    {
        [JsonProperty("Custom.Timesheets.TimesheetRawData")]
        public string TimesheetRawData { get; set; }
        [JsonProperty("System.Title")]
        public string Title { get; set; }
    }

    public class WIQLViewModel
    {
        public string queryType { get; set; }
        public string queryResultType { get; set; }
        public string asOf { get; set; }
        public List<Column> columns { get; set; }
        public List<WorkItem> workItems { get; set; }
    }
    #endregion

    #region Capacity
    public class TfsTeamMember
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }

    public class TfsCapacity
    {
        public double capacityPerDay { get; set; }
        public string name { get; set; }
    }

    public class TfsTeamCapacityViewModel
    {
        public TfsTeamMember teamMember { get; set; }
        public TfsCapacity[] activities { get; set; }
        public TfsDayOff[] daysOff { get; set; }
        public string url { get; set; }
    }

    public class TfsDayOff
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }

    public class TfsTeamCapacityListViewModel
    {
        public int count { get; set; }
        public TfsTeamCapacityViewModel[] value { get; set; }
    }
    #endregion


    #region TfsRootFolder
    public class TfsRootFolderViewModel
    {
        public int version { get; set; }
        public string changeDate { get; set; }
        public string path { get; set; }
        public bool isFolder { get; set; }
        public string url { get; set; }
        public int? size { get; set; }
        public string hashValue { get; set; }
    }

    public class TfsRootFolderListViewModel
    {
        public TfsRootFolderViewModel[] value { get; set; }
        public int count { get; set; }
    }
    #endregion

    #region ChangeSet
    public class Author
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }

    public class CheckedInBy
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }

    public class ChangeSetViewModel
    {
        public int changesetId { get; set; }
        public string url { get; set; }
        public Author author { get; set; }
        public CheckedInBy checkedInBy { get; set; }
        public string createdDate { get; set; }
        public string comment { get; set; }
    }

    public class ChangesetListViewModel
    {
        public int count { get; set; }
        public ChangeSetViewModel[] value { get; set; }
    }

    public class ChangesetWorkItem
    {
        public string webUrl { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string workItemType { get; set; }
        public string state { get; set; }
        public string assignedTo { get; set; }
    }

    public class TfsChangesetWorkItems
    {
        public int count { get; set; }
        public List<ChangesetWorkItem> value { get; set; }
    }

    #endregion


    #region BranchViewModel
    public class Owner
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
    }

    public class BranchViewModel
    {
        public string path { get; set; }
        public Owner owner { get; set; }
        public string createdDate { get; set; }
        public string url { get; set; }
        public List<object> relatedBranches { get; set; }
        public List<object> mappings { get; set; }
    }

    public class BranchListViewModel
    {
        public int count { get; set; }
        public BranchViewModel[] value { get; set; }
    }
    #endregion

    #region AreaPath
    //public class Child3
    //{
    //    public int id { get; set; }
    //    public string identifier { get; set; }
    //    public string name { get; set; }
    //    public string structureType { get; set; }
    //    public bool hasChildren { get; set; }
    //    public string url { get; set; }
    //}

    //public class Child2
    //{
    //    public int id { get; set; }
    //    public string identifier { get; set; }
    //    public string name { get; set; }
    //    public string structureType { get; set; }
    //    public bool hasChildren { get; set; }
    //    public string url { get; set; }
    //    public List<Child3> children { get; set; }
    //}

    //public class Child
    //{
    //    public int id { get; set; }
    //    public string identifier { get; set; }
    //    public string name { get; set; }
    //    public string structureType { get; set; }
    //    public bool hasChildren { get; set; }
    //    public string url { get; set; }
    //    public List<Child2> children { get; set; }
    //}

    //public class Self
    //{
    //    public string href { get; set; }
    //}

    //public class Links
    //{
    //    public Self self { get; set; }
    //}

    public class AreaPathTreeViewModel
    {
        public int id { get; set; }
        public string identifier { get; set; }
        public string name { get; set; }
        public string structureType { get; set; }
        public bool hasChildren { get; set; }
        public AreaPathTreeViewModel[] children { get; set; }
        //public Links _links { get; set; }
        public string url { get; set; }

    }
    #endregion

    public class WorkItemFieldsUpdate
    {
        public string op { get; set; }
        public string path { get; set; }
        public string value { get; set; }
    }

    public class WorkItemBatchPost
    {
        public class BatchRequest
        {
            public string method { get; set; }
            public Dictionary<string, string> headers { get; set; }
            public object[] body { get; set; }
            public string uri { get; set; }
        }
    }
}