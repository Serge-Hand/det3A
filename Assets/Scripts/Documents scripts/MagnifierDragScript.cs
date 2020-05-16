using UnityEngine;

public class MagnifierDragScript : MonoBehaviour
{
	LineRenderer line;

	Vector3 end_line_point;

	Vector3 camPos;

	TimeManager timeMan;

	void Start()
	{
		line = gameObject.AddComponent<LineRenderer>();
		line.startWidth = 0.003f;

		line.startColor = Color.red;
		line.endColor = Color.red;

		timeMan = GameObject.Find("TimeManager").GetComponent<TimeManager>();
		if(timeMan == null)
		{
			Debug.LogError("TimeManager don't found");
			this.enabled = false;
		}
	}

	void OnMouseDown()
	{
		SetDragLine();
		line.enabled = true;
	}

	void OnMouseDrag()
	{
		SetDragLine();
	}

	void OnMouseUp()
	{
		line.enabled = false;

		RaycastHit hit;
		if (Physics.Raycast(camPos, (end_line_point - camPos).normalized, out hit))
			if (hit.collider.gameObject.tag.Equals("Searchable")) // Searchable Tag
			{
				//Debug.LogWarning(hit.collider.gameObject.name); // Сообщение выводится когда линия отпускается над целевым объектом

				hit.transform.Rotate(new Vector3(20, 20, 0));
				//Взять компонент объекта hit. Вызвать функцию. 
				//Эта функция должна 'исследовать' объект, т.е. открывать Точки Интереса, а также блокировать возможность повторного исследования того же объекта

				timeMan.AddMinutesToTime(TimeManager.c_magnifierMinutesPlus); // Количество минут, которые тратятся на исследование лупой задаётся в TimeManager
			}
	}

	void SetDragLine()
	{
		end_line_point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));

		camPos = Camera.main.transform.position;

		line.SetPosition(0, (transform.position - camPos).normalized + camPos);
		line.SetPosition(1, end_line_point);
	}
}
