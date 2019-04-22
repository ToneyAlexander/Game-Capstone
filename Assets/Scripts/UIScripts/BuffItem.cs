using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItem : MonoBehaviour
{
	private TimedBuff _buff;
	public TimedBuff buff
	{
		get { return _buff; }
		set { _buff = value; }
	}
}
