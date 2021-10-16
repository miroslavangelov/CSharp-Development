using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ExportProjectDto
    {
        [XmlElement("TasksCount")]
        public int TasksCount { get; set; }
        
        [XmlElement("ProjectName")]
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string ProjectName { get; set; }

        [XmlElement("HasEndDate")]
        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public List<ExportTaskDto> Tasks { get; set; }
    }
}