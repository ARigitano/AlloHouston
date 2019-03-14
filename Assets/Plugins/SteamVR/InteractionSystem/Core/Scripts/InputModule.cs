using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Linq;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class InputModule : BaseInputModule
	{
		private GameObject submitObject;

		//-------------------------------------------------
		private static InputModule _instance;
		public static InputModule instance
		{
			get
			{
				if ( _instance == null )
					_instance = GameObject.FindObjectOfType<InputModule>();

				return _instance;
			}
		}


		//-------------------------------------------------
		public override bool ShouldActivateModule()
		{
			if ( !base.ShouldActivateModule() )
				return false;

			return submitObject != null;
		}


		//-------------------------------------------------
		public void HoverBegin( GameObject gameObject )
		{
			PointerEventData pointerEventData = new PointerEventData( eventSystem );
			ExecuteEvents.Execute( gameObject, pointerEventData, ExecuteEvents.pointerEnterHandler );
		}


		//-------------------------------------------------
		public void HoverEnd( GameObject gameObject )
		{
			PointerEventData pointerEventData = new PointerEventData( eventSystem );
			pointerEventData.selectedObject = null;
			ExecuteEvents.Execute( gameObject, pointerEventData, ExecuteEvents.pointerExitHandler );
		}

        public void OnBeginDrag( GameObject gameObject, Hand hand)
        {
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            RaycastHit[] hits = Physics.RaycastAll(hand.transform.position, gameObject.transform.position - hand.transform.position, 100.0f);
            Vector3 pressPosition = hits.First(x => x.collider.gameObject == gameObject).point;
            pointerEventData.pressPosition = pressPosition;
            pointerEventData.position = pressPosition;
            ExecuteEvents.Execute(gameObject, pointerEventData, ExecuteEvents.beginDragHandler);
        }

        public void OnEndDrag ( GameObject gameObject, Hand hand)
        {
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            RaycastHit[] hits = Physics.RaycastAll(hand.transform.position, gameObject.transform.position - hand.transform.position, 100.0f);
            Vector3 pressPosition = hits.First(x => x.collider.gameObject == gameObject).point;
            pointerEventData.position = pressPosition;
            ExecuteEvents.Execute(gameObject, pointerEventData, ExecuteEvents.endDragHandler);
        }

        public void OnDragUpdate ( GameObject gameObject, Hand hand)
        {
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            RaycastHit[] hits = Physics.RaycastAll(hand.transform.position, gameObject.transform.position - hand.transform.position, 100.0f);
            Vector3 pressPosition = hits.First(x => x.collider.gameObject == gameObject).point;
            pointerEventData.position = pressPosition;
            ExecuteEvents.Execute(gameObject, pointerEventData, ExecuteEvents.dragHandler );
        }


		//-------------------------------------------------
		public void Submit( GameObject gameObject )
		{
			submitObject = gameObject;
		}


		//-------------------------------------------------
		public override void Process()
		{
			if ( submitObject )
			{
				BaseEventData data = GetBaseEventData();
				data.selectedObject = submitObject;
				ExecuteEvents.Execute( submitObject, data, ExecuteEvents.submitHandler );

				submitObject = null;
			}
		}
	}
}
