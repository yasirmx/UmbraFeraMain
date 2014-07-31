﻿using UnityEngine;
using NodeCanvas.Variables;

namespace NodeCanvas.Conditions{

	[Name("Check Mecanim Float")]
	public class MecanimCheckFloat : MecanimConditions {

		public enum ComparisonTypes{
			EqualTo,
			GreaterThan,
			LessThan
		}

		[RequiredField]
		public string mecanimParameter;
		public ComparisonTypes comparison = ComparisonTypes.EqualTo;
		public BBFloat value;

		protected override string info{
			get
			{
				string comparisonString = "==";
				if (comparison == ComparisonTypes.GreaterThan)
					comparisonString = ">";
				if (comparison == ComparisonTypes.LessThan)
					comparisonString = "<";
				return "Mec.Float '" + mecanimParameter + "' " + comparisonString + " " + value;
			}
		}

		protected override bool OnCheck(){

			if (comparison == ComparisonTypes.GreaterThan)
				return animator.GetFloat(mecanimParameter) > value.value;

			if (comparison == ComparisonTypes.LessThan)
				return animator.GetFloat(mecanimParameter) < value.value;

			return animator.GetFloat(mecanimParameter) == value.value;
		}
	}
}