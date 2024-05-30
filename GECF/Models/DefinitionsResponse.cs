using System;
namespace GECF.Models
{
    public class DefinitionsResponse
    {
        public List<DDatum> data { get; set; }
        public int status { get; set; }
    }

    public class DefinitionList
    {
        public string id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string source { get; set; }
    }

    public class DDatum
    {
        public List<DefinitionList> definition_list { get; set; }
    }
}

