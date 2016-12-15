// ***********************************************************************
// <copyright file="AttributeTests.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading;
using Kore.Attributes;
using Kore.Attributes.Custom;
using Kore.Attributes.Validation;
using Kore.Tests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Tests
{
    /// <summary>
    /// Tests the Attributes part of the core to make sure that the attribute information retrieved remains consistent.
    /// </summary>
    [TestClass]
    public class AttributeTests
    {
        /// <summary>
        /// Tests that the attribute model has correct display values.
        /// </summary>
        [TestMethod]
        public void AttributeModelHasCorrectDisplayValues()
        {
            Dictionary<string, AttributeModel> attrs = typeof(AttributeTestModel).GetAttributes();
            AttributeModel testStartDate = attrs["TestStartDate"];
            Assert.AreEqual("Test Start Date", testStartDate.DisplayName);
            Assert.AreEqual("Test Date Range (Start to End)", testStartDate.DisplayGroupName);
        }

        /// <summary>
        /// Tests that the attribute model the correct default values.
        /// </summary>
        [TestMethod]
        public void AttributeModelHasDefaultValues()
        {
            DateTime testStartDate = DateTime.UtcNow;
            Dictionary<string, AttributeModel> attrs = typeof(AttributeTestModel).GetAttributes();
            Assert.AreEqual("Test Default Value", attrs["TestDefault"].DefaultValue);
            AttributeModel startDateAttr = attrs["TestStartDate"];
            Assert.IsTrue(startDateAttr.DefaultValue is DateTime, "Check default value is datetime");
            var startDate = (DateTime)startDateAttr.DefaultValue;
            Assert.IsTrue(startDate >= testStartDate, "Check that date set is sometime after the start of the test");
            Assert.IsTrue(startDate <= DateTime.UtcNow, "Check that date set is sometime before the end of the test");
        }

        /// <summary>
        /// Tests that the attribute model has "lesser than" validation with correct error message.
        /// </summary>
        [TestMethod]
        public void AttributeModelHasLesserThanValidationWithCorrectErrorMessage()
        {
            Dictionary<string, AttributeModel> attrs = typeof(AttributeTestModel).GetAttributes();
            AttributeModel testAttr = attrs["TestStartDate"];
            Assert.IsTrue(testAttr.Validations.ContainsKey("MustBeLesserThan"));
            ValidationAttribute val = testAttr.Validations["MustBeLesserThan"];
            Assert.AreEqual("Test Start Date must be less than Test End Date", val.ErrorMessage);
        }

        /// <summary>
        /// Tests that the attribute model has "greater than" validation with correct error message.
        /// </summary>
        [TestMethod]
        public void AttributeModelHasGreaterThanValidationWithCorrectErrorMessage()
        {
            Dictionary<string, AttributeModel> attrs = typeof(AttributeTestModel).GetAttributes();
            AttributeModel testAttr = attrs["TestEndDate"];
            Assert.IsTrue(testAttr.Validations.ContainsKey("MustBeGreaterThan"));
            ValidationAttribute val = testAttr.Validations["MustBeGreaterThan"];
            Assert.AreEqual("Test End Date must be greater than Test Start Date", val.ErrorMessage);
        }

        /// <summary>
        /// Tests that the attribute model has "greater than" validation with correct error message in Spanish.
        /// </summary>
        [TestMethod]
        public void AttributeModelGreaterThanMessageIsCorrectInDifferentCulture()
        {
            var savedCulture = Thread.CurrentThread.CurrentUICulture;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");
            Dictionary<string, AttributeModel> attrs = typeof(AttributeTestModel).GetAttributes();
            AttributeModel testAttr = attrs["TestEndDate"];
            Assert.IsTrue(testAttr.Validations.ContainsKey("MustBeGreaterThan"));
            ValidationAttribute val = testAttr.Validations["MustBeGreaterThan"];
            Assert.AreEqual("Prueba Fecha de finalización debe ser mayor que Prueba Fecha de Inicio", val.ErrorMessage);

            // Set it back at the end of the test so it doesn't affect other tests.
            Thread.CurrentThread.CurrentUICulture = savedCulture;
        }

        /// <summary>
        /// Tests that the attribute model has "lesser than or equal" validation with correct error message.
        /// </summary>
        [TestMethod]
        public void AttributeModelHasLesserThanOrEqualValidationWithCorrectErrorMessage()
        {
            Dictionary<string, AttributeModel> attrs = typeof(AttributeTestModel).GetAttributes();
            AttributeModel testAttr = attrs["TestStartNumber"];
            Assert.IsTrue(testAttr.Validations.ContainsKey("MustBeLesserThanOrEqual"));
            ValidationAttribute val = testAttr.Validations["MustBeLesserThanOrEqual"];
            Assert.AreEqual("TestStartNumber must be less than or equal to TestEndNumber", val.ErrorMessage);
        }

        /// <summary>
        /// Tests that the attribute model has "greater than or equal" validation with correct error message.
        /// </summary>
        [TestMethod]
        public void AttributeModelHasGreaterThanOrEqualValidationWithCorrectErrorMessage()
        {
            Dictionary<string, AttributeModel> attrs = typeof(AttributeTestModel).GetAttributes();
            AttributeModel testAttr = attrs["TestEndNumber"];
            Assert.IsTrue(testAttr.Validations.ContainsKey("MustBeGreaterThanOrEqual"));
            ValidationAttribute val = testAttr.Validations["MustBeGreaterThanOrEqual"];
            Assert.AreEqual("TestEndNumber must be greater than or equal to TestStartNumber", val.ErrorMessage);
        }

        /// <summary>
        /// Tests that the attribute model has "must be when" validation with correct error message.
        /// </summary>
        [TestMethod]
        public void MustBeWhenAttributeErrorMessage()
        {
            Dictionary<string, AttributeModel> attrs = typeof(AttributeTestModel).GetAttributes();
            AttributeModel testAttr = attrs["Property1"];
            Assert.IsTrue(testAttr.Validations.ContainsKey("MustBeWhen"));
            ValidationAttribute val = testAttr.Validations["MustBeWhen"];
            Assert.AreEqual("Prop One must be \"One\" when Check Condition One is \"Enabled\"", val.ErrorMessage);
        }

        /// <summary>
        /// Tests that the attribute model has "must be when not" validation with correct error message.
        /// </summary>
        [TestMethod]
        public void MustBeWhenNotAttributeErrorMessage()
        {
            Dictionary<string, AttributeModel> attrs = typeof(AttributeTestModel).GetAttributes();
            AttributeModel testAttr = attrs["Property2"];
            Assert.IsTrue(testAttr.Validations.ContainsKey("MustBeWhenNot"));
            ValidationAttribute val = testAttr.Validations["MustBeWhenNot"];
            Assert.AreEqual("Property2 must be \"1\" unless Condition2 is \"Enabled\"", val.ErrorMessage);
        }

        /// <summary>
        /// Tests that the attribute model has "not allowed when" validation with correct error message.
        /// </summary>
        [TestMethod]
        public void NotAllowedWhenAttributeErrorMessage()
        {
            Dictionary<string, AttributeModel> attrs = typeof(AttributeTestModel).GetAttributes();
            AttributeModel testAttr = attrs["Property3"];
            Assert.IsTrue(testAttr.Validations.ContainsKey("NotAllowedWhen"));
            ValidationAttribute val = testAttr.Validations["NotAllowedWhen"];
            Assert.AreEqual("Property3 cannot be set when Condition3 is \"TRUE\"", val.ErrorMessage);
        }

        /// <summary>
        /// Tests that the attribute model has "not allowed when not" validation with correct error message.
        /// </summary>
        [TestMethod]
        public void NotAllowedWhenNotAttributeErrorMessage()
        {
            Dictionary<string, AttributeModel> attrs = typeof(AttributeTestModel).GetAttributes();
            AttributeModel testAttr = attrs["Property4"];
            Assert.IsTrue(testAttr.Validations.ContainsKey("NotAllowedWhenNot"));
            ValidationAttribute val = testAttr.Validations["NotAllowedWhenNot"];
            Assert.AreEqual("Property4 cannot be set unless Condition4 is \"OK\"", val.ErrorMessage);
        }

        /// <summary>
        /// Tests that the attribute model has "required when" validation with correct error message.
        /// </summary>
        [TestMethod]
        public void RequiredWhenAttributeErrorMessage()
        {
            Dictionary<string, AttributeModel> attrs = typeof(AttributeTestModel).GetAttributes();
            AttributeModel testAttr = attrs["Property5"];
            Assert.IsTrue(testAttr.Validations.ContainsKey("RequiredWhen"));
            ValidationAttribute val = testAttr.Validations["RequiredWhen"];
            Assert.AreEqual("Property5 is required when Condition5 is \"Required\"", val.ErrorMessage);
        }

        /// <summary>
        /// Tests that the attribute model has "required when not" validation with correct error message.
        /// </summary>
        [TestMethod]
        public void RequiredWhenNotAttributeErrorMessage()
        {
            Dictionary<string, AttributeModel> attrs = typeof(AttributeTestModel).GetAttributes();
            AttributeModel testAttr = attrs["Property6"];
            Assert.IsTrue(testAttr.Validations.ContainsKey("RequiredWhenNot"));
            ValidationAttribute val = testAttr.Validations["RequiredWhenNot"];
            Assert.AreEqual("Property6 is required unless Condition6 is \"Optional\"", val.ErrorMessage);
        }

        /// <summary>
        /// Tests that the "less than" validation passes.
        /// </summary>
        [TestMethod]
        public void LessThanValidationIsCorrect()
        {
            var passModel = new AttributeTestModel
            {
                TestStartDate = new DateTime(2016, 1, 1),
                TestEndDate = new DateTime(2016, 2, 1)
            };

            var failModel1 = new AttributeTestModel
            {
                TestStartDate = new DateTime(2016, 1, 1),
                TestEndDate = new DateTime(2016, 1, 1)
            };

            var failModel2 = new AttributeTestModel
            {
                TestStartDate = new DateTime(2016, 2, 1),
                TestEndDate = new DateTime(2016, 1, 1)
            };

            Assert.IsTrue(TryValidateProperty(passModel, "TestStartDate"), "Attribute validation did not pass as expected.");
            Assert.IsFalse(TryValidateProperty(failModel1, "TestStartDate"), "Attribute validation for model 1 did not fail as expected.");
            Assert.IsFalse(TryValidateProperty(failModel2, "TestStartDate"), "Attribute validation for model 2 did not fail as expected.");
        }

        /// <summary>
        /// Tests that the "greater than" validation passes.
        /// </summary>
        [TestMethod]
        public void GreaterThanValidationIsCorrect()
        {
            var passModel = new AttributeTestModel
            {
                TestStartDate = new DateTime(2016, 1, 1),
                TestEndDate = new DateTime(2016, 2, 1)
            };

            var failModel1 = new AttributeTestModel
            {
                TestStartDate = new DateTime(2016, 1, 1),
                TestEndDate = new DateTime(2016, 1, 1)
            };

            var failModel2 = new AttributeTestModel
            {
                TestStartDate = new DateTime(2016, 2, 1),
                TestEndDate = new DateTime(2016, 1, 1)
            };

            Assert.IsTrue(TryValidateProperty(passModel, "TestEndDate"), "Attribute validation did not pass as expected.");
            Assert.IsFalse(TryValidateProperty(failModel1, "TestEndDate"), "Attribute validation for model 1 did not fail as expected.");
            Assert.IsFalse(TryValidateProperty(failModel2, "TestEndDate"), "Attribute validation for model 2 did not fail as expected.");
        }

        /// <summary>
        /// Tests that the "less than or equal" validation passes.
        /// </summary>
        [TestMethod]
        public void LessThanOrEqualValidationIsCorrect()
        {
            var passModel1 = new AttributeTestModel
            {
                TestStartNumber = 1,
                TestEndNumber = 1
            };

            var passModel2 = new AttributeTestModel
            {
                TestStartNumber = 1,
                TestEndNumber = 2
            };

            var failModel = new AttributeTestModel
            {
                TestStartNumber = 2,
                TestEndNumber = 1
            };

            Assert.IsTrue(TryValidateProperty(passModel1, "TestStartNumber"), "Attribute validation for model 1 did not pass as expected.");
            Assert.IsTrue(TryValidateProperty(passModel2, "TestStartNumber"), "Attribute validation for model 2 did not pass as expected.");
            Assert.IsFalse(TryValidateProperty(failModel, "TestStartNumber"), "Attribute validation did not fail as expected.");
        }

        /// <summary>
        /// Tests that the "greater than or equal" validation passes.
        /// </summary>
        [TestMethod]
        public void GreaterThanOrEqualValidationIsCorrect()
        {
            var passModel1 = new AttributeTestModel
            {
                TestStartNumber = 1,
                TestEndNumber = 1
            };

            var passModel2 = new AttributeTestModel
            {
                TestStartNumber = 1,
                TestEndNumber = 2
            };

            var failModel = new AttributeTestModel
            {
                TestStartNumber = 2,
                TestEndNumber = 1
            };

            Assert.IsTrue(TryValidateProperty(passModel1, "TestEndNumber"), "Attribute validation for model 1 did not pass as expected.");
            Assert.IsTrue(TryValidateProperty(passModel2, "TestEndNumber"), "Attribute validation for model 2 did not pass as expected.");
            Assert.IsFalse(TryValidateProperty(failModel, "TestEndNumber"), "Attribute validation did not fail as expected.");
        }

        /// <summary>
        /// Tests that the "must be when" validation passes.
        /// </summary>
        [TestMethod]
        public void MustBeWhenValidationIsCorrect()
        {
            var passModel1 = new AttributeTestModel
            {
                Property1 = 1,
                Condition1 = true
            };

            var passModel2 = new AttributeTestModel
            {
                Property1 = 2,
                Condition1 = false
            };

            var failModel = new AttributeTestModel
            {
                Property1 = 2,
                Condition1 = true
            };

            Assert.IsTrue(TryValidateProperty(passModel1, "Property1"), "Attribute validation for model 1 did not pass as expected.");
            Assert.IsTrue(TryValidateProperty(passModel2, "Property1"), "Attribute validation for model 2 did not pass as expected.");
            Assert.IsFalse(TryValidateProperty(failModel, "Property1"), "Attribute validation did not fail as expected.");
        }

        /// <summary>
        /// Tests that the "must be when not" validation passes.
        /// </summary>
        [TestMethod]
        public void MustBeWhenNotValidationIsCorrect()
        {
            var passModel1 = new AttributeTestModel
            {
                Property2 = 1,
                Condition2 = false
            };

            var passModel2 = new AttributeTestModel
            {
                Property2 = 2,
                Condition2 = true
            };

            var failModel = new AttributeTestModel
            {
                Property2 = 2,
                Condition2 = false
            };
            
            Assert.IsTrue(TryValidateProperty(passModel1, "Property2"), "Attribute validation for model 1 did not pass as expected.");
            Assert.IsTrue(TryValidateProperty(passModel2, "Property2"), "Attribute validation for model 2 did not pass as expected.");
            Assert.IsFalse(TryValidateProperty(failModel, "Property2"), "Attribute validation did not fail as expected.");
        }

        /// <summary>
        /// Tests that the "not allowed when" validation passes.
        /// </summary>
        [TestMethod]
        public void NotAllowedWhenValidationIsCorrect()
        {
            var passModel1 = new AttributeTestModel
            {
                Property3 = 0,
                Condition3 = true
            };

            var passModel2 = new AttributeTestModel
            {
                Property3 = 1,
                Condition3 = false
            };

            var failModel = new AttributeTestModel
            {
                Property3 = 1,
                Condition3 = true
            };

            Assert.IsTrue(TryValidateProperty(passModel1, "Property3"), "Attribute validation for model 1 did not pass as expected.");
            Assert.IsTrue(TryValidateProperty(passModel2, "Property3"), "Attribute validation for model 2 did not pass as expected.");
            Assert.IsFalse(TryValidateProperty(failModel, "Property3"), "Attribute validation did not fail as expected.");
        }

        /// <summary>
        /// Tests that the "not allowed when not" validation passes.
        /// </summary>
        [TestMethod]
        public void NotAllowedWhenNotValidationIsCorrect()
        {
            var passModel1 = new AttributeTestModel
            {
                Property4 = null,
                Condition4 = "Bad"
            };

            var passModel2 = new AttributeTestModel
            {
                Property4 = "allowed",
                Condition4 = "OK"
            };

            var failModel = new AttributeTestModel
            {
                Property4 = "not allowed",
                Condition4 = "Bad"
            };

            Assert.IsTrue(TryValidateProperty(passModel1, "Property4"), "Attribute validation for model 1 did not pass as expected.");
            Assert.IsTrue(TryValidateProperty(passModel2, "Property4"), "Attribute validation for model 2 did not pass as expected.");
            Assert.IsFalse(TryValidateProperty(failModel, "Property4"), "Attribute validation did not fail as expected.");
        }

        /// <summary>
        /// Tests that the "required when" validation passes.
        /// </summary>
        [TestMethod]
        public void RequiredWhenValidationIsCorrect()
        {
            var passModel1 = new AttributeTestModel
            {
                Property5 = 1.1M,
                Condition5 = "Required"
            };

            var passModel2 = new AttributeTestModel
            {
                Property5 = null,
                Condition5 = "Optional"
            };

            var failModel = new AttributeTestModel
            {
                Property5 = null,
                Condition5 = "Required"
            };

            Assert.IsTrue(TryValidateProperty(passModel1, "Property5"), "Attribute validation for model 1 did not pass as expected.");
            Assert.IsTrue(TryValidateProperty(passModel2, "Property5"), "Attribute validation for model 2 did not pass as expected.");
            Assert.IsFalse(TryValidateProperty(failModel, "Property5"), "Attribute validation did not fail as expected.");
        }

        /// <summary>
        /// Tests that the "required when not" validation passes.
        /// </summary>
        [TestMethod]
        public void RequiredWhenNotValidationIsCorrect()
        {
            var passModel1 = new AttributeTestModel
            {
                Property6 = 1.1M,
                Condition6 = "Required"
            };

            var passModel2 = new AttributeTestModel
            {
                Property6 = 0,
                Condition6 = "Optional"
            };

            var failModel = new AttributeTestModel
            {
                Property6 = 0,
                Condition6 = "asdf"
            };

            Assert.IsTrue(TryValidateProperty(passModel1, "Property6"), "Attribute validation for model 1 did not pass as expected.");
            Assert.IsTrue(TryValidateProperty(passModel2, "Property6"), "Attribute validation for model 2 did not pass as expected.");
            Assert.IsFalse(TryValidateProperty(failModel, "Property6"), "Attribute validation did not fail as expected.");
        }

        /// <summary>
        /// Tries to validate the property.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if valid, <c>false</c> otherwise.</returns>
        private bool TryValidateProperty(AttributeTestModel model, string propertyName)
        {
            Type t = typeof(AttributeTestModel);
            var ctx = new ValidationContext(model);
            ctx.MemberName = propertyName;
            return Validator.TryValidateProperty(t.GetProperty(propertyName).GetValue(model), ctx, null);
        }
    }

    /// <summary>
    /// A model for testing the attribute code
    /// </summary>
    public class AttributeTestModel
    {
        /// <summary>
        /// Gets or sets the test start date.
        /// </summary>
        /// <value>The test start date.</value>
        [DefaultDateNow]
        [Display(ResourceType = typeof(TestResources), Name = "TestStartDate", GroupName = "TestDateRangeGroupName")]
        [MustBeLesserThan(OtherPropertyName = "TestEndDate", OtherPropertyResourceType = typeof(TestResources), OtherPropertyDisplayName = "TestEndDate")]
        public DateTime TestStartDate { get; set; }

        /// <summary>
        /// Gets or sets the test end date.
        /// </summary>
        /// <value>The test end date.</value>
        [Display(ResourceType = typeof(TestResources), Name = "TestEndDate", GroupName = "TestDateRangeGroupName")]
        [MustBeGreaterThan(OtherPropertyName = "TestStartDate", OtherPropertyResourceType = typeof(TestResources), OtherPropertyDisplayName = "TestStartDate")]
        public DateTime TestEndDate { get; set; }

        /// <summary>
        /// Gets or sets the test start number.
        /// </summary>
        /// <value>The test start number.</value>
        [MustBeLesserThanOrEqual(OtherPropertyName = "TestEndNumber")]
        public int TestStartNumber { get; set; }

        /// <summary>
        /// Gets or sets the test end number.
        /// </summary>
        /// <value>The test end number.</value>
        [MustBeGreaterThanOrEqual(OtherPropertyName = "TestStartNumber")]
        public int TestEndNumber { get; set; }

        /// <summary>
        /// Gets or sets the test default.
        /// </summary>
        /// <value>The test default.</value>
        [DefaultValue("Test Default Value")]
        public string TestDefault { get; set; }

        /// <summary>
        /// Gets or sets the property1.
        /// </summary>
        /// <value>The property1.</value>
        [Display(ResourceType = typeof(TestResources), Name = "Property1")]
        [MustBeWhen(RequiredPropertyValue = 1, RequiredPropertyValueForError = "One", OtherPropertyName = "Condition1", OtherPropertyResourceType = typeof(TestResources), OtherPropertyValue = true, OtherPropertyValueForError = "Enabled")]
        public int Property1 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AttributeTestModel"/> is condition1.
        /// </summary>
        /// <value><c>true</c> if condition1; otherwise, <c>false</c>.</value>
        public bool Condition1 { get; set; }

        /// <summary>
        /// Gets or sets the property2.
        /// </summary>
        /// <value>The property2.</value>
        [MustBeWhenNot(RequiredPropertyValue = 1, OtherPropertyName = "Condition2", OtherPropertyValue = true, OtherPropertyValueForError = "Enabled")]
        public int Property2 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AttributeTestModel"/> is condition2.
        /// </summary>
        /// <value><c>true</c> if condition2; otherwise, <c>false</c>.</value>
        public bool Condition2 { get; set; }

        /// <summary>
        /// Gets or sets the property3.
        /// </summary>
        /// <value>The property3.</value>
        [NotAllowedWhen(OtherPropertyName = "Condition3", OtherPropertyValue = true, OtherPropertyValueForError = "TRUE")]
        public int Property3 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AttributeTestModel"/> is condition3.
        /// </summary>
        /// <value><c>true</c> if condition3; otherwise, <c>false</c>.</value>
        public bool Condition3 { get; set; }

        /// <summary>
        /// Gets or sets the property4.
        /// </summary>
        /// <value>The property4.</value>
        [NotAllowedWhenNot(OtherPropertyName = "Condition4", OtherPropertyValue = "OK")]
        public string Property4 { get; set; }

        /// <summary>
        /// Gets or sets the condition4.
        /// </summary>
        /// <value>The condition4.</value>
        public string Condition4 { get; set; }

        /// <summary>
        /// Gets or sets the property5.
        /// </summary>
        /// <value>The property5.</value>
        [RequiredWhen(OtherPropertyName = "Condition5", OtherPropertyValue = "Required")]
        public decimal? Property5 { get; set; }

        /// <summary>
        /// Gets or sets the condition5.
        /// </summary>
        /// <value>The condition5.</value>
        public string Condition5 { get; set; }

        /// <summary>
        /// Gets or sets the property6.
        /// </summary>
        /// <value>The property6.</value>
        [RequiredWhenNot(OtherPropertyName = "Condition6", OtherPropertyValue = "Optional")]
        public decimal Property6 { get; set; }

        /// <summary>
        /// Gets or sets the condition6.
        /// </summary>
        /// <value>The condition6.</value>
        public string Condition6 { get; set; }
    }
}
