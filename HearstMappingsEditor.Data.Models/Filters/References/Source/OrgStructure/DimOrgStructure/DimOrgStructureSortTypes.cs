using System;

namespace HearstMappingsEditor.Data.Models
{
    [Obsolete("Table splitted due to normalization")]
    public enum DimOrgStructureSortTypes
    {
        AllOrgStructureID,
        AllOrgStructure,
        PerimeterLawID,
        PerimeterLawDesc,
        PerimeterLawCode,
        PerimeterID,
        PerimeterDesc,
        PerimeterCode,
        PerimeterCurrency,
        CompanyID,
        CompanyDesc,
        CompanyCode,
        DateStart,
        DateEnd,
        CreateDate
    }
}
