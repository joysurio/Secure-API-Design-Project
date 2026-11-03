using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LFS.Shared.Models
{
    public class DatatableAria
    {
        public string sortAscending { get; set; }
        public string sortDescending { get; set; }
    }

    public class DatatableArray
    {
        public string without { get; set; }
        public string notEmpty { get; set; }
        public string not { get; set; }
        public string contains { get; set; }
        public string empty { get; set; }
        public string equals { get; set; }
    }

    public class DatatableAutoFill
    {
        public string cancel { get; set; }
        public string fill { get; set; }
        public string fillHorizontal { get; set; }
        public string fillVertical { get; set; }
    }

    public class DatatableButton
    {
        public string _0 { get; set; }
        public string _ { get; set; }
    }

    public class DatatableButtons
    {
        public string collection { get; set; }
        public string colvis { get; set; }
        public string colvisRestore { get; set; }
        public string copy { get; set; }
        public string copyKeys { get; set; }
        public DatatableCopySuccess copySuccess { get; set; }
        public string copyTitle { get; set; }
        public string csv { get; set; }
        public string excel { get; set; }
        public DatatablePageLength pageLength { get; set; }
        public string pdf { get; set; }
        public string print { get; set; }
        public string updateState { get; set; }
        public string stateRestore { get; set; }
        public string savedStates { get; set; }
        public string renameState { get; set; }
        public string removeState { get; set; }
        public string removeAllStates { get; set; }
        public string createState { get; set; }
    }

    public class DatatableCells
    {
        public string _1 { get; set; }
        public string _ { get; set; }
    }

    public class DatatableCollapse
    {
        public string _0 { get; set; }
        public string _ { get; set; }
    }

    public class DatatableColumns
    {
        public string _1 { get; set; }
        public string _ { get; set; }
        public string visible { get; set; }
        public string search { get; set; }
    }

    public class DatatableConditions
    {
        public DatatableDate date { get; set; }
        public DatatableNumber number { get; set; }
        public DatatableString @string { get; set; }
        public DatatableArray array { get; set; }
    }

    public class DatatableConfirm
    {
        public string _ { get; set; }
        public string _1 { get; set; }
    }

    public class DatatableCopySuccess
    {
        public string _1 { get; set; }
        public string _ { get; set; }
    }

    public class DatatableCreate
    {
        public string button { get; set; }
        public string title { get; set; }
        public string submit { get; set; }
    }

    public class DatatableCreationModal
    {
        public string toggleLabel { get; set; }
        public string title { get; set; }
        public string select { get; set; }
        public string searchBuilder { get; set; }
        public string search { get; set; }
        public string scroller { get; set; }
        public string paging { get; set; }
        public string order { get; set; }
        public string name { get; set; }
        public DatatableColumns columns { get; set; }
        public string button { get; set; }
    }

    public class DatatableDate
    {
        public string after { get; set; }
        public string before { get; set; }
        public string between { get; set; }
        public string empty { get; set; }
        public string equals { get; set; }
        public string not { get; set; }
        public string notBetween { get; set; }
        public string notEmpty { get; set; }
    }

    public class DatatableDatetime
    {
        public string previous { get; set; }
        public string next { get; set; }
        public string hours { get; set; }
        public string minutes { get; set; }
        public string seconds { get; set; }
        public string unknown { get; set; }
        public List<string> amPm { get; set; }
        public List<string> weekdays { get; set; }
        public List<string> months { get; set; }
    }

    public class DatatableEdit
    {
        public string button { get; set; }
        public string title { get; set; }
        public string submit { get; set; }
    }

    public class DatatableEditor
    {
        public string close { get; set; }
        public DatatableCreate create { get; set; }
        public DatatableEdit edit { get; set; }
        public DatatableRemove remove { get; set; }
        public DatatableError error { get; set; }
        public DatatableMulti multi { get; set; }
    }

    public class DatatableError
    {
        public string system { get; set; }
    }

    public class DatatableMulti
    {
        public string title { get; set; }
        public string info { get; set; }
        public string restore { get; set; }
        public string noMulti { get; set; }
    }

    public class DatatableNumber
    {
        public string between { get; set; }
        public string empty { get; set; }
        public string equals { get; set; }
        public string gt { get; set; }
        public string gte { get; set; }
        public string lt { get; set; }
        public string lte { get; set; }
        public string not { get; set; }
        public string notBetween { get; set; }
        public string notEmpty { get; set; }
    }

    public class DatatablePageLength
    {
        [JsonProperty("-1")]
        public string _1 { get; set; }
        public string _ { get; set; }
    }

    public class DatatablePaginate
    {
        public string first { get; set; }
        public string last { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
    }

    public class DatatableRemove
    {
        public string button { get; set; }
        public string title { get; set; }
        public string submit { get; set; }
        public DatatableConfirm confirm { get; set; }
    }

    public class DatatableSearchBuilder
    {
        public string add { get; set; }
        public DatatableButton button { get; set; }
        public string clearAll { get; set; }
        public string condition { get; set; }
        public DatatableConditions conditions { get; set; }
        public string data { get; set; }
        public string deleteTitle { get; set; }
        public string leftTitle { get; set; }
        public string logicAnd { get; set; }
        public string logicOr { get; set; }
        public string rightTitle { get; set; }
        public DatatableTitle title { get; set; }
        public string value { get; set; }
    }

    public class DatatableSearchPanes
    {
        public string clearMessage { get; set; }
        public DatatableCollapse collapse { get; set; }
        public string count { get; set; }
        public string countFiltered { get; set; }
        public string emptyPanes { get; set; }
        public string loadMessage { get; set; }
        public string title { get; set; }
        public string showMessage { get; set; }
        public string collapseMessage { get; set; }
    }

    public class DatatableSelect
    {
        public DatatableCells cells { get; set; }
        public DatatableColumns columns { get; set; }
    }

    public class DatatableStateRestore
    {
        public string renameTitle { get; set; }
        public string renameLabel { get; set; }
        public string renameButton { get; set; }
        public string removeTitle { get; set; }
        public string removeSubmit { get; set; }
        public string removeJoiner { get; set; }
        public string removeError { get; set; }
        public string removeConfirm { get; set; }
        public string emptyStates { get; set; }
        public string emptyError { get; set; }
        public string duplicateError { get; set; }
        public DatatableCreationModal creationModal { get; set; }
    }

    public class DatatableString
    {
        public string contains { get; set; }
        public string empty { get; set; }
        public string endsWith { get; set; }
        public string equals { get; set; }
        public string not { get; set; }
        public string notEmpty { get; set; }
        public string startsWith { get; set; }
        public string notContains { get; set; }
        public string notStarts { get; set; }
        public string notEnds { get; set; }
    }

    public class DatatableTitle
    {
        public string _0 { get; set; }
        public string _ { get; set; }
    }

    public class DatatableLocalizationModel
    {
        public string emptyTable { get; set; }
        public string info { get; set; }
        public string infoEmpty { get; set; }
        public string infoFiltered { get; set; }
        public string infoThousands { get; set; }
        public string lengthMenu { get; set; }
        public string loadingRecords { get; set; }
        public string processing { get; set; }
        public string search { get; set; }
        public string zeroRecords { get; set; }
        public string thousands { get; set; }
        public DatatablePaginate paginate { get; set; }
        public DatatableAria aria { get; set; }
        public DatatableAutoFill autoFill { get; set; }
        public DatatableButtons buttons { get; set; }
        public DatatableSearchBuilder searchBuilder { get; set; }
        public DatatableSearchPanes searchPanes { get; set; }
        public DatatableSelect select { get; set; }
        public DatatableDatetime datetime { get; set; }
        public DatatableEditor editor { get; set; }
        public DatatableStateRestore stateRestore { get; set; }
         
    }
}
