using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Rank.Forms
{
    public class DataValidator
    {
        public RangeValidator GetRangeValidator(string rangeValidatorId, string fieldToValidateId, string type)
        {

            RangeValidator fieldRangeValidator = new RangeValidator();
            fieldRangeValidator.ID = rangeValidatorId;
            fieldRangeValidator.ControlToValidate = fieldToValidateId;
            fieldRangeValidator.ForeColor = Color.Red;
            fieldRangeValidator.Enabled = false;
            switch (type)
            {

                case "int": //int
                    {
                        fieldRangeValidator.MinimumValue = int.MinValue.ToString();
                        fieldRangeValidator.MaximumValue = int.MaxValue.ToString();
                        fieldRangeValidator.Type = ValidationDataType.Integer;
                        fieldRangeValidator.ErrorMessage = "!";
                        fieldRangeValidator.Enabled = true;
                        break;
                    }
                case "float": //double
                    {
                        //fieldRangeValidator.MinimumValue = double.MinValue.ToString();
                        //fieldRangeValidator.MaximumValue = double.MaxValue.ToString();
                        fieldRangeValidator.Type = ValidationDataType.Double;
                        fieldRangeValidator.ErrorMessage = "!";
                        fieldRangeValidator.Enabled = true;
                        break;
                    }
                case "date": //date
                    {
                        fieldRangeValidator.MinimumValue = "1/1/1900";
                        fieldRangeValidator.MaximumValue = "1/1/2090";
                        fieldRangeValidator.Type = ValidationDataType.Date;
                        fieldRangeValidator.ErrorMessage = "!";
                        fieldRangeValidator.Enabled = true;
                        break;
                    }
                case "string": //text
                    {
                        //fieldRangeValidator.MinimumValue = double.MinValue.ToString();
                        //fieldRangeValidator.MaximumValue = double.MaxValue.ToString();
                        //fieldRangeValidator.ErrorMessage = "Только текст";
                        fieldRangeValidator.Enabled = false;
                        fieldRangeValidator.Type = ValidationDataType.String;
                        //fieldRangeValidator.Enabled = true;
                        break;
                    }
                default:
                    {

                        break;
                    }
            }
            return fieldRangeValidator;
        }
        public TextBox GetTextBox(string type)
        {
            TextBox textBoxToReturn = new TextBox();
            switch (type)
            {

                case "int": //int
                    {
                        break;
                    }
                case "float": //double
                    {

                        break;
                    }
                case "date": //date
                    {
                        textBoxToReturn.Attributes.Add("onfocus", "this.select();lcs(this)");
                        textBoxToReturn.Attributes.Add("onclick", "event.cancelBubble=true;this.select();lcs(this)");
                        break;
                    }
                case "string": //text
                    {
                        textBoxToReturn.TextMode = TextBoxMode.SingleLine;
                        break;
                    }
            }
            return textBoxToReturn;
        }
        }
    }
