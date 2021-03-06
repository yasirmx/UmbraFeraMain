﻿using UnityEngine;
using NodeCanvas.Variables;

namespace NodeCanvas.Conditions{

	[Category("✫ Blackboard")]
	public class CheckString : ConditionTask {

		[BlackboardOnly]
		public BBString stringA;
		public BBString stringB;

		protected override string info{
			get {return stringA + " == " + stringB;}
		}

		protected override bool OnCheck(){
			return stringA.value == stringB.value;
		}
	}
}