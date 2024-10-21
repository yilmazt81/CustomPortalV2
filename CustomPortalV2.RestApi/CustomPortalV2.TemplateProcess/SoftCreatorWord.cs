using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using RunProperties = DocumentFormat.OpenXml.Wordprocessing.RunProperties;
using Strike = DocumentFormat.OpenXml.Wordprocessing.Strike;
using DocumentFormat.OpenXml.Wordprocessing;
using Bold = DocumentFormat.OpenXml.Wordprocessing.Bold;
using FontSize = DocumentFormat.OpenXml.Wordprocessing.FontSize;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;
using TableStyle = DocumentFormat.OpenXml.Wordprocessing.TableStyle;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using TableCell = DocumentFormat.OpenXml.Wordprocessing.TableCell;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using TableCellProperties = DocumentFormat.OpenXml.Wordprocessing.TableCellProperties;
using TableRow = DocumentFormat.OpenXml.Wordprocessing.TableRow;
using TableProperties = DocumentFormat.OpenXml.Wordprocessing.TableProperties;
using TopBorder = DocumentFormat.OpenXml.Wordprocessing.TopBorder;
using BottomBorder = DocumentFormat.OpenXml.Wordprocessing.BottomBorder;
using LeftBorder = DocumentFormat.OpenXml.Wordprocessing.LeftBorder;
using RightBorder = DocumentFormat.OpenXml.Wordprocessing.RightBorder;
using InsideHorizontalBorder = DocumentFormat.OpenXml.Wordprocessing.InsideHorizontalBorder;
using InsideVerticalBorder = DocumentFormat.OpenXml.Wordprocessing.InsideVerticalBorder;
using Italic = DocumentFormat.OpenXml.Wordprocessing.Italic;
using Break = DocumentFormat.OpenXml.Wordprocessing.Break;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.Core.Model.Form;

namespace CustomPortalV2.TemplateProcess
{
    public class SoftCreatorWord : IDisposable
    {
        bool findStart = false;
        bool findEnd = false;

        string boxMarked = "⌧";
        string boxUnmarked = "☐";
        // CustomEntities customEntities = null;

        private void UnderLine(Run run)
        {
            if (run.RunProperties == null)
            {
                run.RunProperties = new RunProperties();
            }
            run.RunProperties.Strike = new Strike();
            run.RunProperties.Strike.Val = true;
            // run.RunProperties.Underline = new Underline();
            // run.RunProperties.Underline.Val = UnderlineValues.Single;
        }

        public void MergeInNewFile(string resultFile, IList<string> filenames)
        {
            using (WordprocessingDocument document = WordprocessingDocument.Create(resultFile, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = document.AddMainDocumentPart();
                mainPart.Document = new Document(new Body());

                foreach (string filename in filenames)
                {
                    AlternativeFormatImportPart chunk = mainPart.AddAlternativeFormatImportPart(AlternativeFormatImportPartType.WordprocessingML);
                    string altChunkId = mainPart.GetIdOfPart(chunk);

                    using (FileStream fileStream = File.Open(filename, FileMode.Open))
                    {
                        chunk.FeedData(fileStream);
                    }

                    AltChunk altChunk = new AltChunk { Id = altChunkId };
                    mainPart.Document.Body.AppendChild(altChunk);
                }

                mainPart.Document.Save();
            }
        }

        private Run GetTextElement(string value, int fontSize = 16, bool bold = false, bool italic = false, string fontFamiliy = "Times New Roman")
        {
            var textElement = new Text();
            textElement.Text = (string.IsNullOrEmpty(value) ? "  " : value);
            StyleRunProperties stylRunProps = new StyleRunProperties();
            stylRunProps.FontSize = new FontSize() { Val = (fontSize == 0 ? "16" : fontSize.ToString()) };

            if (bold)
            {
                stylRunProps.Append(new Bold());
            }
            if (italic)
            {
                stylRunProps.Append(new Italic());
            }
            var runFont = new RunFonts { Ascii = fontFamiliy };

            stylRunProps.RunFonts = runFont;
            var runElement = new Run();
            runElement.Append(stylRunProps);
            runElement.Append(textElement);

            return runElement;
        }

        private Run GetTextElement(string[] value, int fontSize = 16, bool bold = false, bool italic = false)
        {

            StyleRunProperties stylRunProps = new StyleRunProperties();
            stylRunProps.FontSize = new FontSize() { Val = (fontSize == 0 ? "16" : fontSize.ToString()) };

            if (bold)
            {
                stylRunProps.Append(new Bold());
            }
            if (italic)
            {
                stylRunProps.Append(new Italic());
            }
            var runFont = new RunFonts { Ascii = "Times New Roman" };

            stylRunProps.RunFonts = runFont;

            var runElement = new Run();
            runElement.Append(stylRunProps);
            foreach (var oneLine in value)
            {
                var textElement = new Text();
                textElement.Text = (string.IsNullOrEmpty(oneLine) ? "  " : oneLine);
                runElement.Append(textElement);

                runElement.Append(new Break());
            }


            return runElement;
        }
        private void AddTextToForm(FormDefinationField formDefination,
            BookmarkEnd bookmarkEnd,
            int customeFieldTypeId,
            FormMetaDataAttribute_CustomeField[] formMetaDataAttribute_Customes, CustomeFieldItem[] customeFieldItemList)

        {
            var customeFieldItems = customeFieldItemList.Where(s => s.CustomeFieldId == customeFieldTypeId).ToList();
            if (customeFieldItems.Count() == 0)
                return;
            var firstTag = customeFieldItems.FirstOrDefault();

            if (formMetaDataAttribute_Customes.Count(s => s.TagName == firstTag.TagName) == 0)
                return;
            var haveAttacment = formMetaDataAttribute_Customes.Any(s => s.Attachment);
            if (haveAttacment)
            {
                AddTextFieldToForm(bookmarkEnd, "Contents of attached list", formDefination.FontSize, formDefination.Bold, formDefination.Italic);
                return;
            }

            var rowCount = formMetaDataAttribute_Customes.Where(s => s.TagName == firstTag.TagName).Max(s => s.DataOrder);
            //ilk formlarda 1 row olacak
            if (rowCount == 0)
            {
                rowCount = 1;
            }
            var longFieldValue = string.Empty;

            for (int i = 0; i <= rowCount; i++)
            {
                var firstField = customeFieldItems.FirstOrDefault();
                var firstFieldValue = formMetaDataAttribute_Customes.FirstOrDefault(s => s.FormDefinationFieldId == formDefination.Id && s.TagName == firstField.TagName && s.DataOrder == i);
                if (firstFieldValue == null)
                {
                    continue;
                }

                foreach (var customeField in customeFieldItems)
                {
                    var formMetaDataAttribute = formMetaDataAttribute_Customes.FirstOrDefault(s => s.FormDefinationFieldId == formDefination.Id && s.TagName == customeField.TagName && s.DataOrder == i);
                    string formValue = (formMetaDataAttribute == null ? String.Empty : formMetaDataAttribute.FieldValue);
                    longFieldValue += formValue + ",";

                }
            }
            if (longFieldValue.Length > 0)
            {
                longFieldValue = longFieldValue.Substring(0, longFieldValue.Length - 1);
            }
            AddTextFieldToForm(bookmarkEnd, longFieldValue, formDefination.FontSize, formDefination.Bold, formDefination.Italic);
        }
        private string GetFormMetaDataValue(FormMetaDataAttribute[] metaDataAttributes,
            FormDefinationField[] formDefinationFields,
            string fieldName, ComboBoxItem[] comboBoxItems)
        {
            var fieldType = formDefinationFields.SingleOrDefault(s => s.TagName == fieldName);
            string returnV = string.Empty;
            if (fieldType != null)
            {
                if (fieldType.ControlType == "CheckBox" || fieldType.ControlType == "RadioBox")
                {
                    var checkItems = comboBoxItems.Where(s => s.ItemType == fieldType.TagName).ToArray();
                    foreach (var oneItem in checkItems)
                    {
                        string checkBoxTagName = fieldType.TagName + "_" + oneItem.TagName;

                        var fieldValue = metaDataAttributes.FirstOrDefault(s => s.TagName == checkBoxTagName);
                        if (fieldValue != null)
                        {
                            if (fieldValue.FieldValue == "true")
                            {
                                returnV += " " + oneItem.Name;
                            }
                        }
                    }
                }
                else
                {
                    var formMetaAttribute = metaDataAttributes.FirstOrDefault(s => s.TagName == fieldName);
                    returnV += (formMetaAttribute == null ? String.Empty : formMetaAttribute.FieldValue);
                }
            }

            return returnV;
        }
        private string GetFieldValue(string fieldName, int rowNumber,
            FormMetaDataAttribute[] metaDataAttributes, FormMetaDataAttribute_CustomeField[] attribute_CustomeFields,
            FormDefinationField[] formDefinationFields, ComboBoxItem[] comboBoxItems)
        {
            if (string.IsNullOrEmpty(fieldName))
                return string.Empty;
            var fieldParts = fieldName.Split(' ');

            string returnV = string.Empty;
            foreach (var oneField in fieldParts)
            {
                if (oneField.StartsWith("{") && oneField.EndsWith("}"))
                {
                    var justFieldName = oneField.Substring(1, oneField.Length - 2);

                    var attributeValue = attribute_CustomeFields.FirstOrDefault(s => s.DataOrder == rowNumber && s.TagName == justFieldName);
                    if (attributeValue != null)
                    {
                        returnV += " " + attributeValue.FieldValue;
                    }
                    else
                    {
                        returnV += " " + GetFormMetaDataValue(metaDataAttributes, formDefinationFields, justFieldName, comboBoxItems);

                    }

                }
                else
                {
                    var attributeValue = attribute_CustomeFields.FirstOrDefault(s => s.DataOrder == rowNumber && s.TagName == oneField);
                    if (attributeValue != null)
                    {
                        returnV += " " + attributeValue.FieldValue;
                    }
                    else
                    {
                        returnV += " " + GetFormMetaDataValue(metaDataAttributes, formDefinationFields, oneField, comboBoxItems);
                    }
                }
            }

            return returnV;
        }

        private bool IsEmptyFielValue(string fieldName, int rowNumber,
            FormMetaDataAttribute[] metaDataAttributes,
            FormMetaDataAttribute_CustomeField[] attribute_CustomeFields,
            FormDefinationField[] formDefinationFields)
        {
            var fieldParts = fieldName.Split(' ');

            if (fieldName.Contains("{"))
            {
                foreach (var oneField in fieldParts)
                {
                    if (oneField.StartsWith("{") && oneField.EndsWith("}"))
                    {
                        var justFieldName = oneField.Substring(1, oneField.Length - 2);

                        var attributeValue = attribute_CustomeFields.FirstOrDefault(s => s.DataOrder == rowNumber && s.TagName == justFieldName);
                        if (attributeValue != null)
                        {
                            if (!string.IsNullOrEmpty(attributeValue.FieldValue))
                                return false;
                        }

                    }
                }
            }
            else
            {
                var attributeValue = attribute_CustomeFields.FirstOrDefault(s => s.DataOrder == rowNumber && s.TagName == fieldName);
                if (attributeValue != null)
                {
                    if (!string.IsNullOrEmpty(attributeValue.FieldValue))
                        return false;
                }
            }


            return true;
        }
        private void AddTableToForm(
            FormDefinationField formDefination,
            BookmarkEnd bookmarkEnd,
            int customeFieldTypeId,
             FormMetaDataAttribute[] formMetaDataAttributes,
            FormMetaDataAttribute_CustomeField[] formMetaDataAttribute_Customes,
            FormDefinationField[] formDefinationFields,
            int formDefinationTypeId,
            int formAttachmentId,
            int formVersionId,
            CustomeFieldItem[] customeFieldItemList,
             CustomeField_VirtualTable[] queryVirtualTable,
            CustomeField_VirtualTableField[] customeField_VirtualTableFields,
            ComboBoxItem[] comboBoxItems)
        {

            var customeFieldItems = customeFieldItemList.Where(s => s.CustomeFieldId == customeFieldTypeId).ToList();
            var customeFiel = customeFieldItemList.Single(s => s.Id == customeFieldTypeId);

            CustomeField_VirtualTable? virtualTable = null;
            if (formAttachmentId != 0)
            {
                virtualTable = queryVirtualTable.FirstOrDefault(a => a.FormDefinationAttachmentId == formAttachmentId && a.CustomeFieldId == customeFieldTypeId);

            }
            else if (formVersionId != 0)
            {

                virtualTable = queryVirtualTable.FirstOrDefault(a => a.FormVersionId == formVersionId && a.CustomeFieldId == customeFieldTypeId);
            }
            else if (formDefinationTypeId != 0)
            {

                virtualTable = queryVirtualTable.FirstOrDefault(a => a.FormDefinationId == formDefinationTypeId && a.CustomeFieldId == customeFieldTypeId);
            }

            List<TableHeader> tableHeaders = new List<TableHeader>();
            if (virtualTable != null)
            {
                var virtualTableHeader = customeField_VirtualTableFields.Where(s => s.CustomeField_VirtualTableId == virtualTable.Id).ToList();
                tableHeaders = virtualTable.CustomeField_VirtualTableField.Select(s => new TableHeader()
                {
                    Caption = s.HeaderText,
                    FieldName = s.CalculateField,
                    HeaderHeightRule = virtualTable.HeaderHeightRuleValue,
                    HeaderWithRule = s.HeaderWidthRuleValue,
                    With = s.HeaderWidth,
                    Height = virtualTable.HeaderHeight,
                    RowHeightRule = virtualTable.RowHeightRuleValue,
                    RowHeightValue = virtualTable.RowHeight,
                }).ToList();
            }
            else
            {
                tableHeaders = customeFieldItems.Select(s => new TableHeader()
                {
                    Caption = s.FieldCaption,
                    FieldName = s.TagName,
                    HeaderHeightRule = (customeFiel == null ? 10 : customeFiel.HeaderHeightRuleValue),
                    HeaderWithRule = s.HeaderWidthRuleValue,
                    With = s.HeaderWidth,
                    Height = customeFiel.HeaderHeight,
                    RowHeightRule = customeFiel.RowHeightRuleValue,
                    RowHeightValue = customeFiel.RowHeight,
                    Bold = s.Bold,
                    FontSize = s.FontSize,
                    Italic = s.Italic,
                }).ToList();

            }
            var firstTag = customeFieldItems.FirstOrDefault();
            if (formMetaDataAttribute_Customes.Count(s => s.TagName == firstTag.TagName) == 0)
            {
                return;
            }

            var rowCount = formMetaDataAttribute_Customes.Where(s => s.TagName == firstTag.TagName).Max(s => s.DataOrder);
            //ilk formlarda 1 row olacak
            if (rowCount == 0)
            {
                rowCount = 1;
            }

            var haveAttacment = formMetaDataAttribute_Customes.Any(s => s.Attachment);
            if (haveAttacment)
            {
                AddTextFieldToForm(bookmarkEnd, "Contents of attached list", formDefination.FontSize, formDefination.Bold, formDefination.Italic);
                return;
            }
            List<TableValue> tableValues = new List<TableValue>();
            int internalRow = 0;
            for (int i = 0; i <= rowCount; i++)
            {

                var firstField = tableHeaders.Where(s => s.FieldName.Length > 2).FirstOrDefault();
                if (IsEmptyFielValue(firstField.FieldName, i, formMetaDataAttributes, formMetaDataAttribute_Customes, formDefinationFields))
                    continue;
                int valueCounter = 0;
                foreach (var customeField in tableHeaders)
                {
                    var fieldName = customeField.FieldName;


                    var fieldValue = GetFieldValue(fieldName, i, formMetaDataAttributes, formMetaDataAttribute_Customes, formDefinationFields, comboBoxItems);

                    tableValues.Add(new TableValue()
                    {
                        RowNumber = internalRow,
                        ColNumber = valueCounter,
                        FieldValue = fieldValue,
                        Bold = customeField.Bold,
                        Italic = customeField.Italic,
                        FontSize = customeField.FontSize
                    });
                    valueCounter++;
                }
                internalRow++;


            }
            AddTableWithCustomeField(bookmarkEnd, tableHeaders.ToArray(), tableValues.ToArray(), (virtualTable == null ? 0 : virtualTable.TableBorder.Value));

        }

        private void AddTableTextToForm(
        FormDefinationField formDefination,
        BookmarkEnd bookmarkEnd,
        int customeFieldTypeId,
         FormMetaDataAttribute[] formMetaDataAttributes,
        FormMetaDataAttribute_CustomeField[] formMetaDataAttribute_Customes,
        FormDefinationField[] formDefinationFields,
        int formDefinationTypeId,
        int formAttachmentId,
        int formVersionId,
        CustomeFieldItem[] customeFieldItemList,
        CustomeField[] customeFields,
        CustomeField_VirtualTable[] queryVirtualTable,
        CustomeField_VirtualTableField[] customeField_VirtualTableFields,
        ComboBoxItem[] comboBoxItems)
        {

            var customeFieldItems = customeFieldItemList.Where(s => s.CustomeFieldId == customeFieldTypeId && !s.Deleted).OrderBy(s => s.OrderNumber).ToList();
            var customeFiel = customeFields.Single(s => s.Id == customeFieldTypeId);


            CustomeField_VirtualTable? virtualTable = null;

            if (formAttachmentId != 0)
            {
                virtualTable = queryVirtualTable.FirstOrDefault(a => a.FormDefinationAttachmentId == formAttachmentId && a.CustomeFieldId == customeFieldTypeId && !a.Deleted);

            }
            else if (formVersionId != 0)
            {

                virtualTable = queryVirtualTable.FirstOrDefault(a => a.FormVersionId == formVersionId && a.CustomeFieldId == customeFieldTypeId && !a.Deleted);
            }
            else if (formDefinationTypeId != 0)
            {

                virtualTable = queryVirtualTable.FirstOrDefault(a => a.FormDefinationId == formDefinationTypeId && a.CustomeFieldId == customeFieldTypeId && !a.Deleted);
            }

            List<TableHeader> tableHeaders = new List<TableHeader>();
            if (virtualTable != null)
            {
                var virtualTableHeader = customeField_VirtualTableFields.Where(s => s.CustomeField_VirtualTableId == virtualTable.Id).ToList();
                tableHeaders = virtualTable.CustomeField_VirtualTableField.Select(s => new TableHeader()
                {
                    Caption = s.HeaderText,
                    FieldName = s.CalculateField,
                    HeaderHeightRule = virtualTable.HeaderHeightRuleValue,
                    HeaderWithRule = s.HeaderWidthRuleValue,
                    With = s.HeaderWidth,
                    Height = virtualTable.HeaderHeight,
                    RowHeightRule = virtualTable.RowHeightRuleValue,
                    RowHeightValue = virtualTable.RowHeight,
                }).ToList();
            }
            else
            {
                tableHeaders = customeFieldItems.Select(s => new TableHeader()
                {
                    Caption = s.FieldCaption,
                    FieldName = s.TagName,
                    HeaderHeightRule = customeFiel.HeaderHeightRuleValue,
                    HeaderWithRule = s.HeaderWidthRuleValue,
                    With = s.HeaderWidth,
                    Height = customeFiel.HeaderHeight,
                    RowHeightRule = customeFiel.RowHeightRuleValue,
                    RowHeightValue = customeFiel.RowHeight,
                    Bold = s.Bold,
                    FontSize = s.FontSize,
                    Italic = s.Italic,
                }).ToList();

            }
            var firstTag = customeFieldItems.FirstOrDefault();
            if (formMetaDataAttribute_Customes.Count(s => s.TagName == firstTag.TagName) == 0)
            {
                return;
            }

            var rowCount = formMetaDataAttribute_Customes.Where(s => s.TagName == firstTag.TagName).Max(s => s.DataOrder);
            //ilk formlarda 1 row olacak
            if (rowCount == 0)
            {
                rowCount = 1;
            }

            var haveAttacment = formMetaDataAttribute_Customes.Any(s => s.Attachment);
            if (haveAttacment)
            {
                AddTextFieldToForm(bookmarkEnd, "Contents of attached list", formDefination.FontSize, formDefination.Bold, formDefination.Italic);
                return;
            }
            List<TableValue> tableValues = new List<TableValue>();
            int internalRow = 0;
            var fontSize = 0;
            bool fontbold = false, fontitalic = false;
            for (int i = 0; i <= rowCount; i++)
            {

                var firstField = tableHeaders.Where(s => s.FieldName.Length > 2).FirstOrDefault();
                if (IsEmptyFielValue(firstField.FieldName, i, formMetaDataAttributes, formMetaDataAttribute_Customes, formDefinationFields))
                    continue;
                int valueCounter = 0;
                foreach (var customeField in tableHeaders)
                {
                    var fieldName = customeField.FieldName;

                    fontSize = customeField.FontSize.Value;
                    fontbold = customeField.Bold.Value;
                    fontitalic = customeField.Italic.Value;
                    var fieldValue = GetFieldValue(fieldName, i, formMetaDataAttributes, formMetaDataAttribute_Customes, formDefinationFields, comboBoxItems);

                    tableValues.Add(new TableValue()
                    {
                        RowNumber = internalRow,
                        ColNumber = valueCounter,
                        FieldValue = fieldValue.Trim(),
                        Bold = customeField.Bold,
                        Italic = customeField.Italic,
                        FontSize = customeField.FontSize
                    });
                    valueCounter++;
                }
                internalRow++;

            }

            List<string> lLines = new List<string>();
            int t = 0;
            foreach (TableValue tableValue in tableValues)
            {
                string oneLineStr = string.Empty;
                string tableStr = string.Empty;
                int colnumber = 0;
                foreach (var customeField in tableHeaders)
                {

                    var fieldValue = GetFieldValue(customeField.FieldName, t, formMetaDataAttributes, formMetaDataAttribute_Customes, formDefinationFields, comboBoxItems);

                    fieldValue = fieldValue.PadRight(int.Parse(customeField.With), ' ');
                    tableStr += fieldValue;
                    colnumber++;

                }
                if (string.IsNullOrEmpty(tableStr.Trim()))
                    continue;
                tableStr = (t + 1).ToString().PadRight(5, ' ') + tableStr;
                lLines.Add(tableStr);
                t++;

            }
            bookmarkEnd.InsertAfterSelf(GetTextElement(lLines.ToArray(), fontSize, fontbold, fontitalic));


        }
        private void AddTableWithCustomeField(BookmarkEnd bookmarkEnd, TableHeader[] tableHeaders, TableValue[] tableValues, int border)
        {

            Table tbl = new Table();
            string lineNumberW = "20";
            TableProperties properties = new TableProperties();
            //set the table width to page width
            TableWidth tableWidth = new TableWidth() { Width = "90%", Type = TableWidthUnitValues.Pct };
            if (border != 0)
            {
                TableBorders tableBorders = new TableBorders(
                    new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size = (UInt32)border },
                    new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size = (UInt32)border },
                    new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size = (UInt32)border },
                    new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size = (UInt32)border },
                    new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size = (UInt32)border },
                    new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Thick), Size = (UInt32)border });

                properties.Append(tableBorders);
            }

            properties.Append(tableWidth);
            //add properties to table
            tbl.Append(properties);
            TableRow trHeader = new TableRow();

            int headerNumber = 0;
            foreach (var customeField in tableHeaders)
            {
                HeightRuleValues heightRule = (customeField.HeaderHeightRule == null ? HeightRuleValues.Exact : new HeightRuleValues(customeField.HeaderHeightRule.Value.ToString()));
                TableWidthUnitValues widthUnitValues = (customeField.HeaderWithRule == null ? TableWidthUnitValues.Dxa : new TableWidthUnitValues(customeField.HeaderWithRule.Value.ToString()));

                if (headerNumber == 0)
                {
                    TableCell tcHeaderLine = new TableCell(new Paragraph(GetTextElement("Sıra", (customeField.FontSize == null ? 16 : customeField.FontSize.Value), true, false)));
                    tcHeaderLine.Append(new TableCellProperties(new TableRowHeight() { HeightType = heightRule, Val = (UInt32?)customeField.Height.Value }));
                    tcHeaderLine.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Nil, Width = lineNumberW }));
                    trHeader.Append(tcHeaderLine);
                }
                string headerText = customeField.Caption;
                TableCell tcHeader = new TableCell(new Paragraph(GetTextElement(headerText, (customeField.FontSize == null ? 16 : customeField.FontSize.Value), true, false)));

                tcHeader.Append(new TableCellProperties(new TableRowHeight() { HeightType = heightRule, Val = (UInt32?)customeField.Height.Value }));
                tcHeader.Append(new TableCellProperties(new TableCellWidth() { Type = widthUnitValues, Width = customeField.With }));

                trHeader.Append(tcHeader);
                headerNumber++;
            }
            tbl.AppendChild(trHeader);
            if (tableValues.Length == 0)
                return;
            int rowCount = tableValues.Max(s => s.RowNumber);
            int lineNumber = 1;
            for (int i = 0; i <= rowCount; i++)
            {

                TableRow trLine = new TableRow();
                TableCell tcLineNumber = new TableCell(new Paragraph(GetTextElement(lineNumber.ToString())));
                tcLineNumber.Append(new TableCellProperties(new TableRowHeight() { HeightType = HeightRuleValues.Auto }));
                tcLineNumber.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Nil, Width = lineNumberW }));
                trLine.Append(tcLineNumber);




                int colnumber = 0;
                foreach (var customeField in tableHeaders)
                {
                    TableWidthUnitValues widthUnitValues = (customeField.HeaderWithRule == null ? TableWidthUnitValues.Dxa : new TableWidthUnitValues(customeField.HeaderWithRule.Value.ToString()));
                    HeightRuleValues heightRule = (customeField.RowHeightRule == null ? HeightRuleValues.Exact : new HeightRuleValues(customeField.RowHeightRule.Value.ToString()));

                    //var formMetaDataAttribute = formMetaDataAttribute_Customes.FirstOrDefault(s => s.FormDefinationFieldId == formDefination.Id && s.TagName == customeField.TagName && s.DataOrder == i);
                    string formValue = tableValues.FirstOrDefault(s => s.RowNumber == i && s.ColNumber == colnumber).FieldValue;
                    TableCell tcHeader = new TableCell(new Paragraph(GetTextElement(formValue, (customeField.FontSize == null ? 16 : customeField.FontSize.Value), (customeField.Bold == null ? false : customeField.Bold.Value), (customeField.Italic == null ? false : customeField.Italic.Value))));
                    tcHeader.Append(new TableCellProperties(new TableRowHeight() { HeightType = heightRule, Val = (UInt32?)tableHeaders[i].RowHeightValue }));
                    //  tcHeader.Append(new TableCellProperties(new TableCellWidth() { Type = widthUnitValues, Width = tableHeaders[i].With }));

                    trLine.Append(tcHeader);
                    colnumber++;
                }
                tbl.AppendChild(trLine);
                lineNumber++;
            }

            var runElement = new Run();
            runElement.Append(tbl);
            bookmarkEnd.InsertAfterSelf(runElement);
        }
        private void AddTextFieldToForm(BookmarkEnd bookmarkEnd, string fieldValue, int? fontsize, bool? bold, bool? italic)
        {
            bookmarkEnd.InsertAfterSelf(GetTextElement(fieldValue, (fontsize == null ? 16 : fontsize.Value), (bold == null ? false : bold.Value), (italic == null ? false : italic.Value)));
        }

        private void AddTextFieldToForm(BookmarkEnd bookmarkEnd, string fieldValue, int? fontsize, bool? bold, bool? italic, string fontFamily)
        {
            bookmarkEnd.InsertAfterSelf(GetTextElement(fieldValue, (fontsize == null ? 16 : fontsize.Value), (bold == null ? false : bold.Value), (italic == null ? false : italic.Value), fontFamily));
        }

        public string[] GetTagList(string filePath)
        {
            OpenSettings os = new OpenSettings
            {
                AutoSave = false
            };

            string[] allKeys = null;
            using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(filePath, true, os))
            {
                var findBook = FindBookmarks(wordprocessingDocument.MainDocumentPart.Document.Body);

                allKeys = findBook.Keys.Select(s => s).ToArray();

            }

            return allKeys;
        }

        private string TranslateText(string language, string value, TranslateDictionary[] translateDictionaries)
        {
            if (string.IsNullOrEmpty(language))
                return value;
            var searhParam = Helper.ToUpperTrk(value);
            var translateText = translateDictionaries.FirstOrDefault(s => s.Language == language && s.SearchText == searhParam);
            if (translateText == null)
            {
                return value;
            }
            else
            {
                return translateText.Translate;
            }
        }


        private void SetCheckBoxRulesToForm(SoftImageChangeFormatRule[] softImageChangeFormats, Dictionary<string, BookmarkEnd> findBook, string value)
        {
            foreach (var oneRule in softImageChangeFormats)
            {
                var currentBookMark = findBook.FirstOrDefault(s => s.Key == oneRule.StartTag);
                if (currentBookMark.Key == null)
                    continue;

                AddTextFieldToForm(currentBookMark.Value, (oneRule.FieldValue == value ? boxMarked : boxUnmarked), 15, false, false, (oneRule.FieldValue == value ? "Cambria Math" : "Segoe UI Symbol"));
            }
        }
        public void ChangeTemplateForm(string formName,
            FormDefinationField[] formDefinationFields,
            FormMetaDataAttribute_CustomeField[] formMetaDataAttribute_Customes,
            FormMetaDataAttribute[] formMetaDataAttributes,
            SoftImageChangeFormatRule[] softImageChangeFormatRules,
            int formDefinationTypeId,
            int formAttachmentId,
            int formVersionId,
            FormAttachmentFontStyle[] formAttachmentFontStyles,
            ComboBoxItem[] comboBoxItems,
            CustomeField[] customeFields,
              CustomeFieldItem[] customeFieldItemList,
         CustomeField_VirtualTable[] queryVirtualTable,
        CustomeField_VirtualTableField[] customeField_VirtualTableFields,
        TranslateDictionary[] translateDictionaries,
        FormDefinationAttachment formDefinationAttachment = null)
        {
            OpenSettings os = new OpenSettings
            {
                AutoSave = true
            };

            // customEntities = pcustomEntities;

            using (var wordprocessingDocument = WordprocessingDocument.Open(formName, true))
            {

                var findBook = FindBookmarks(wordprocessingDocument.MainDocumentPart.Document.Body);

                int? fontSize = null;
                bool? bold = null;
                bool? italic = null;
                string fontFamily = "Times New Roman";

                foreach (var formDefination in formDefinationFields)
                {
                    fontSize = formDefination.FontSize;
                    bold = formDefination.Bold;
                    italic = formDefination.Italic;
                    string translateText = formDefination.TranslateLanguage;
                    if (formAttachmentId != 0)
                    {
                        var formDefinationFieldCustomeStyle = formAttachmentFontStyles.FirstOrDefault(s => s.TagName == formDefination.TagName && s.FormDefinationAttachmentId == formAttachmentId);

                        if (formDefinationFieldCustomeStyle != null)
                        {
                            translateText = formDefinationFieldCustomeStyle.TranslateLanguage;

                            fontSize = formDefinationFieldCustomeStyle.FontSize;
                            bold = formDefinationFieldCustomeStyle.Bold;
                            italic = formDefinationFieldCustomeStyle.Italic;
                            fontFamily = formDefinationFieldCustomeStyle.FontFamily;
                        }
                        else
                        {
                            if (formDefinationAttachment != null)
                            {
                                fontSize = formDefinationAttachment.FontSize;
                                bold = formDefinationAttachment.Bold;
                                italic = formDefinationAttachment.Italic;
                                fontFamily = formDefinationAttachment.FontFamily;

                            }
                        }
                    }

                    if (formDefination.ControlType == "Text" || formDefination.ControlType == "DateTime" || formDefination.ControlType == "Hidden")
                    {
                        var currentBookMark = findBook.FirstOrDefault(s => s.Key == formDefination.TagName);
                        if (currentBookMark.Key == null)
                            continue;
                        var fieldValue = formMetaDataAttributes.FirstOrDefault(s => s.TagName == formDefination.TagName);
                        var applyCheckBoxRule = softImageChangeFormatRules.Where(s => s.FieldName == formDefination.TagName && s.Operation == "Checkbox").ToArray();

                        if (fieldValue == null)
                        {
                            SetCheckBoxRulesToForm(applyCheckBoxRule, findBook, string.Empty);
                            continue;
                        }

                        AddTextFieldToForm(currentBookMark.Value, TranslateText(translateText, fieldValue.FieldValue, translateDictionaries), fontSize, bold, italic, fontFamily);

                        var upperStr = fieldValue.FieldValue.ToUpperTrk();
                        foreach (var oneRule in softImageChangeFormatRules.Where(s => s.FieldName == formDefination.TagName && s.FieldValue == upperStr && s.Operation == "inLine"))
                        {
                            findStart = false;
                            findEnd = false;
                            CheckFormatOnLine(wordprocessingDocument.MainDocumentPart.Document.Body, oneRule.StartTag, oneRule.EndTag, oneRule);
                        }

                        SetCheckBoxRulesToForm(applyCheckBoxRule, findBook, upperStr);
                        continue;


                    }
                    else if (formDefination.ControlType == "CheckBox" || formDefination.ControlType == "RadioBox")
                    {
                        var checkItems = comboBoxItems.Where(s => s.ItemType == formDefination.TagName).ToArray();
                        var checboxTextBookMark = findBook.FirstOrDefault(s => s.Key == formDefination.TagName);
                        foreach (var oneItem in checkItems)
                        {
                            string checkBoxTagName = formDefination.TagName + "_" + oneItem.TagName;
                            var checkBookMark = findBook.FirstOrDefault(s => s.Key == checkBoxTagName);

                            var fieldValue = formMetaDataAttributes.FirstOrDefault(s => s.TagName == checkBoxTagName);
                            if (fieldValue != null)
                            {
                                if (fieldValue.FieldValue == "true")
                                {
                                    if (checkBookMark.Key != null)
                                    {
                                        AddTextFieldToForm(checkBookMark.Value, "X", fontSize, true, false);
                                    }
                                    if (checboxTextBookMark.Key != null)
                                    {
                                        AddTextFieldToForm(checboxTextBookMark.Value, oneItem.Name, formDefination.FontSize, formDefination.Bold, formDefination.Italic, fontFamily);
                                    }
                                }
                                else
                                {
                                    if (checkBookMark.Key != null)
                                    {
                                        AddTextFieldToForm(checkBookMark.Value, " ", fontSize, false, false);
                                    }
                                }

                                var upperStr = fieldValue.FieldValue;
                                foreach (var oneRule in softImageChangeFormatRules.Where(s => s.FieldName == checkBoxTagName && s.FieldValue == upperStr && s.Operation == "inLine"))
                                {
                                    findStart = false;
                                    findEnd = false;
                                    CheckFormatOnLine(wordprocessingDocument.MainDocumentPart.Document.Body, oneRule.StartTag, oneRule.EndTag, oneRule);
                                }
                            }

                        }
                    }
                    else if (formDefination.ControlType == "ComboBox")
                    {
                        var checboxTextBookMark = findBook.FirstOrDefault(s => s.Key == formDefination.TagName);

                        string checkBoxTagName = formDefination.TagName;
                        var checkBookMark = findBook.FirstOrDefault(s => s.Key == checkBoxTagName);

                        var fieldValue = formMetaDataAttributes.FirstOrDefault(s => s.TagName == checkBoxTagName);
                        if (fieldValue == null || string.IsNullOrEmpty(fieldValue.FieldValue))
                        {
                            continue;
                        }
                        var checkItem = comboBoxItems.FirstOrDefault(s => s.ItemType == formDefination.TagName && s.TagName == fieldValue.FieldValue);

                        if (checkItem == null)
                        {
                            continue;
                        }

                        if (fieldValue != null)
                        {
                            if (checkBookMark.Key != null)
                            {
                                AddTextFieldToForm(checkBookMark.Value, checkItem.Name, fontSize, bold, italic, fontFamily);
                            }


                            var upperStr = fieldValue.FieldValue;
                            foreach (var oneRule in softImageChangeFormatRules.Where(s => s.FieldName == checkBoxTagName && s.FieldValue == upperStr && s.Operation == "inLine"))
                            {
                                findStart = false;
                                findEnd = false;
                                CheckFormatOnLine(wordprocessingDocument.MainDocumentPart.Document.Body, oneRule.StartTag, oneRule.EndTag, oneRule);
                            }
                        }
                    }
                    else
                    {
                        var customeControlType = customeFields.SingleOrDefault(s => s.FieldTagName == formDefination.ControlType);
                        if (customeControlType != null)
                        {
                            var tableMark = findBook.FirstOrDefault(s => s.Key == formDefination.TagName);
                            //var controlTypeFields = customEntities.CustomeFieldItem.Where(s => s.CustomeFieldId == customeControlType.Id && !s.Deleted.Value).ToArray();
                            if (tableMark.Value != null)
                            {
                                if (customeControlType.ElementType == "Table")
                                {
                                    AddTableToForm(formDefination, tableMark.Value,
                                        customeControlType.Id, formMetaDataAttributes,
                                        formMetaDataAttribute_Customes.Where(s => s.FormDefinationFieldId == formDefination.Id).ToArray(),
                                        formDefinationFields, formDefinationTypeId,
                                        formAttachmentId,
                                        formVersionId, customeFieldItemList, queryVirtualTable, customeField_VirtualTableFields, comboBoxItems);
                                }
                                else if (customeControlType.ElementType == "Text")
                                {
                                    AddTextToForm(formDefination, tableMark.Value, customeControlType.Id, formMetaDataAttribute_Customes.Where(s => s.FormDefinationFieldId == formDefination.Id).ToArray(), customeFieldItemList);
                                }
                                else if (customeControlType.ElementType == "TableText")
                                {
                                    AddTableTextToForm(formDefination, tableMark.Value,
                                       customeControlType.Id, formMetaDataAttributes,
                                       formMetaDataAttribute_Customes.Where(s => s.FormDefinationFieldId == formDefination.Id).ToArray(),
                                       formDefinationFields, formDefinationTypeId,
                                       formAttachmentId,
                                       formVersionId, customeFieldItemList, customeFields, queryVirtualTable, customeField_VirtualTableFields, comboBoxItems);
                                }
                            }
                        }
                    }


                }
                var pageFields = findBook.Keys.Where(s => s.StartsWith("Sayfa")).ToArray();
                //Diger sayfalardaki bilgileri ana formdaki alandan alacaktir.
                foreach (var pageField in pageFields)
                {

                    var firstIndex = pageField.IndexOf("_");
                    firstIndex++;
                    var fieldName = pageField.Substring(firstIndex, pageField.Length - firstIndex);
                    var fieldValue = formMetaDataAttributes.FirstOrDefault(s => s.TagName == fieldName);
                    if (fieldValue == null)
                        continue;
                    var bookMark = findBook.FirstOrDefault(s => s.Key == pageField);
                    var formdefinationField = formDefinationFields.FirstOrDefault(s => s.TagName == fieldName);
                    var fontSizeCustome = formdefinationField.FontSize;
                    var boldCustome = formdefinationField.Bold;
                    var italicCustome = formdefinationField.Italic;
                    string translateLanguage = formdefinationField.TranslateLanguage;
                    if (formAttachmentId != 0)
                    {
                        var formDefinationFieldCustomeStyle = formAttachmentFontStyles.FirstOrDefault(s => s.TagName == pageField && s.FormDefinationAttachmentId == formAttachmentId);

                        if (formDefinationFieldCustomeStyle != null)
                        {
                            fontSizeCustome = formDefinationFieldCustomeStyle.FontSize;
                            boldCustome = formDefinationFieldCustomeStyle.Bold;
                            italicCustome = formDefinationFieldCustomeStyle.Italic;
                            translateLanguage = formDefinationFieldCustomeStyle.TranslateLanguage;
                        }
                    }

                    AddTextFieldToForm(bookMark.Value, TranslateText(translateLanguage, fieldValue.FieldValue, translateDictionaries), fontSizeCustome, boldCustome, italicCustome);
                }

                var todays = findBook.Keys.Where(s => s.Contains("Today") || s.Contains("Yesterday") || s.Contains("Tomorrow")).ToArray();
                foreach (var onekey in todays)
                {
                    var bookMark = findBook.FirstOrDefault(s => s.Key == onekey);
                    string dateValue = DateTime.Today.ToString("dd.MM.yyyy");
                    if (onekey.Contains("Yesterday"))
                    {
                        dateValue = DateTime.Today.AddDays(-1).ToString("dd.MM.yyyy");
                    }
                    else if (onekey.Contains("Tomorrow"))
                    {
                        dateValue = DateTime.Today.AddDays(1).ToString("dd.MM.yyyy");
                    }
                    AddTextFieldToForm(bookMark.Value, dateValue, fontSize, bold, italic);
                }

                wordprocessingDocument.Save();
            }
        }


        private void CheckFormatOnLine(OpenXmlElement documentPart, string start, string end, SoftImageChangeFormatRule formatRule)
        {
            if (documentPart.ChildElements.Count == 0)
                return;
            foreach (var child in documentPart.ChildElements)
            {
                if (child is BookmarkStart)
                {
                    var bStart = child as BookmarkStart;
                    if (bStart.Name == start)
                    {
                        findStart = true;
                    }
                }
                if (child is BookmarkStart)
                {
                    var bStart = child as BookmarkStart;
                    if (bStart.Name == end)
                    {
                        findEnd = true;
                        findStart = false;
                    }
                }
                if (findStart && !findEnd)
                {
                    //Test
                    if (child is Run)
                    {
                        var run = child as Run;
                        if (formatRule.Operation == "inLine")
                        {
                            UnderLine(run);
                        }
                    }
                }
                if (findEnd && findEnd)
                {
                    return;
                }

                CheckFormatOnLine(child, start, end, formatRule);
            }
        }



        private Dictionary<string, BookmarkEnd> FindBookmarks(OpenXmlElement documentPart, Dictionary<string, BookmarkEnd> results = null, Dictionary<string, string> unmatched = null)
        {
            results = results ?? new Dictionary<string, BookmarkEnd>();
            unmatched = unmatched ?? new Dictionary<string, string>();

            foreach (var child in documentPart.Elements())
            {
                if (child is BookmarkStart)
                {
                    var bStart = child as BookmarkStart;
                    try
                    {
                        unmatched.Add(bStart.Id, bStart.Name);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                }

                if (child is BookmarkEnd)
                {
                    var bEnd = child as BookmarkEnd;
                    foreach (var orphanName in unmatched)
                    {
                        if (bEnd.Id == orphanName.Key)
                        {
                            try
                            {
                                results.Add(orphanName.Value, bEnd);
                            }
                            catch (Exception ex)
                            {

                                System.Diagnostics.Debug.WriteLine(ex);
                            }
                        }
                    }
                }

                FindBookmarks(child, results, unmatched);
            }

            return results;
        }

        public void Dispose()
        {

        }
    }
}