﻿using UnityEngine;
using System.Collections;

namespace NodeCanvas.BehaviourTrees{

	[AddComponentMenu("")]
	[Name("Access")]
	[Category("Decorators")]
	[Description("Execute and return the child node status if the condition is true, otherwise return Failure. The condition is evaluated only once in the first Tick and when the node is not already Running unless it is set as 'Dynamic' in which case it will revaluate even while running")]
	[Icon("Accessor")]
	public class BTAccessor : BTDecorator, ITaskAssignable {

		public bool dynamic;

		[SerializeField]
		private ConditionTask _condition;
		private bool accessed;

		public Task task{
			get {return condition;}
			set {condition = (ConditionTask)value;}
		}

		private ConditionTask condition{
			get {return _condition;}
			set
			{
				_condition = value;
				if (_condition != null)
					_condition.SetOwnerSystem(graph);
			}
		}

		protected override Status OnExecute(Component agent, Blackboard blackboard){

			if (!decoratedConnection)
				return Status.Resting;

			if (!condition)
				return decoratedConnection.Execute(agent, blackboard);

			if (dynamic)
			{
				if (condition.CheckCondition(agent, blackboard))
					return decoratedConnection.Execute(agent, blackboard);
				decoratedConnection.ResetConnection();
				return Status.Failure;
			}
			else
			{
				if (status != Status.Running && condition.CheckCondition(agent, blackboard))
					accessed = true;

				return accessed? decoratedConnection.Execute(agent, blackboard) : Status.Failure;
			}
		}

		protected override void OnReset(){
			accessed = false;
		}

		////////////////////////////////////////
		///////////GUI AND EDITOR STUFF/////////
		////////////////////////////////////////
		#if UNITY_EDITOR
		
		protected override void OnNodeInspectorGUI(){

			dynamic = UnityEditor.EditorGUILayout.Toggle("Dynamic", dynamic);

			EditorUtils.Separator();

			if (condition == null){
				EditorUtils.TaskSelectionButton(gameObject, typeof(ConditionTask), delegate(Task c){condition = (ConditionTask)c;});
			} else {
				EditorUtils.TaskTitlebar(condition);
			}
		}
		
		#endif
	}
}