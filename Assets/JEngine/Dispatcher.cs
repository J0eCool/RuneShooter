using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Dispatcher<Base, Responder> : SingletonComponent<Base>
		where Base : Dispatcher<Base, Responder> {
	protected List<Responder> responders = new List<Responder>();
	protected delegate void DispatchMethod(Responder responder);

	public void Subscribe(Responder responder) {
		responders.Add(responder);
		PostSubscribe(responder);
	}

	protected virtual void PostSubscribe(Responder responder) { }

	public void Unsubscribe(Responder responder) {
		responders.Remove(responder);
	}

	protected void Dispatch(DispatchMethod method) {
		foreach (Responder responder in responders) {
			method(responder);
		}
	}
}
