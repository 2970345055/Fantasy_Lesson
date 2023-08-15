#if FANTASY_NET
using OfficeOpenXml;

namespace Fantasy.Helper;

/// <summary>
/// �ṩ���� Excel �ļ��ĸ���������
/// </summary>
public static class ExcelHelper
{
    /// <summary>
    /// ���� Excel �ļ������� ExcelPackage ʵ����
    /// </summary>
    /// <param name="name">Excel �ļ���·����</param>
    /// <returns>ExcelPackage ʵ����</returns>
    public static ExcelPackage LoadExcel(string name)
    {
        return new ExcelPackage(name);
    }

    /// <summary>
    /// ��ȡָ����������ָ������λ�õĵ�Ԫ��ֵ��
    /// </summary>
    /// <param name="sheet">Excel ������</param>
    /// <param name="row">��������</param>
    /// <param name="column">��������</param>
    /// <returns>��Ԫ��ֵ��</returns>
    public static string GetCellValue(this ExcelWorksheet sheet, int row, int column)
    {
        ExcelRange cell = sheet.Cells[row, column];
            
        try
        {
            if (cell.Value == null)
            {
                return "";
            }

            string s = cell.GetValue<string>();
                
            return s.Trim();
        }
        catch (Exception e)
        {
            throw new Exception($"Rows {row} Columns {column} Content {cell.Text} {e}");
        }
    }
}
#endif