using UnityEngine;
using TMPro;

public class InterestPointsDragScript : MonoBehaviour
{
	LineRenderer line;

	Vector3 start_line_point;
	Vector3 end_line_point;

	Vector3 camPos;

	TextMeshPro tmp_text = null;
	TextMeshProUGUI tmp_text_ugui = null;

	int first_link_index;

	void Start()
	{
		line = gameObject.AddComponent<LineRenderer>();
		line.startWidth = 0.003f;

		line.startColor = Color.red;
		line.endColor = Color.red;
		line.enabled = false;

		tmp_text = GetComponent<TextMeshPro>();
		if (tmp_text == null)
		{
			tmp_text_ugui = GetComponent<TextMeshProUGUI>();
		}
	}
	
	private void Update() 
	{
		if(Input.GetMouseButtonDown(0))
		{
			WhenMouseDown();
		}

		if(Input.GetMouseButton(0))
		{
			WhenMouseDrag();
		}

		if (Input.GetMouseButtonUp(0))
		{
			WhenMouseUp();
		}
	} 

	void WhenMouseDown() // Если нажата ЛКМ и Если она попадает на ссылку - Запоминаем начальную точку линии; Включаем линию и Запоминаем данную ссылку (по индексу) 
	{
		if (tmp_text != null)
		{
			int link_index = TMP_TextUtilities.FindIntersectingLink(tmp_text, Input.mousePosition, Camera.main);
			if (link_index != -1)
			{
				//1 - Запоминаем позицию мыши как начальную точку для линии
				start_line_point = end_line_point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
				line.SetPosition(0, start_line_point);
				line.SetPosition(1, end_line_point);
				line.enabled = true;
				first_link_index = link_index;
			}
		}
		else
		{
			Camera cam = null;
			if (!tmp_text_ugui.canvas.renderMode.Equals(RenderMode.ScreenSpaceOverlay))
			{
				if (tmp_text_ugui.canvas.renderMode.Equals(RenderMode.ScreenSpaceCamera))
				{
					cam = tmp_text_ugui.canvas.worldCamera;
				}
				else
				{
					cam = Camera.main;
				}
			}
				int link_index = TMP_TextUtilities.FindIntersectingLink(tmp_text_ugui, Input.mousePosition, cam);
			if (link_index != -1)
			{
				//1 - Запоминаем позицию мыши как начальную точку для линии
				start_line_point = end_line_point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
				line.SetPosition(0, start_line_point);
				line.SetPosition(1, end_line_point);
				line.enabled = true;
				first_link_index = link_index;
			}
		}
	}

	void WhenMouseDrag() // Отрисовка линии 
	{
		if (line.enabled)
		{
			end_line_point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
			line.SetPosition(1, end_line_point);
		}
	}

	void WhenMouseUp() // Окончание рисования линии; Если попадает на некую ссылку - взять её и выполнить Нужные действия 
	{
		int second_link_index;

		line.enabled = false;

		camPos = Camera.main.transform.position;

		RaycastHit hit;
		if (Physics.Raycast(camPos, (end_line_point - camPos).normalized, out hit))
			if (hit.collider.gameObject.tag.Equals("POICarrier")) // POICarrier Tag
			{
				TextMeshPro tmp_text_2 = hit.transform.GetComponentInChildren<TextMeshPro>();
				if (tmp_text_2 != null)
				{
					second_link_index = TMP_TextUtilities.FindIntersectingLink(tmp_text_2, Input.mousePosition, Camera.main);
					if (second_link_index != -1)
					{
						//2 - Выполнять необходимые действия - Сначала анализ ссылок, а затем Создание заметок и т.д.

						string link_one_text = tmp_text.textInfo.linkInfo[first_link_index].GetLinkText();
						string link_two_text = tmp_text_2.textInfo.linkInfo[second_link_index].GetLinkText();

						Debug.Log(link_one_text + " + " + link_two_text);
						//tmp_text.textInfo.linkInfo[first_link_index].
					}
				}
				else
				{
					TextMeshProUGUI tmp_text_ugui_2 = hit.transform.GetComponentInChildren<TextMeshProUGUI>();
					if(tmp_text_ugui_2 != null)
					{
						Camera cam = null;
						if (!tmp_text_ugui_2.canvas.renderMode.Equals(RenderMode.ScreenSpaceOverlay))
						{
							if (tmp_text_ugui_2.canvas.renderMode.Equals(RenderMode.ScreenSpaceCamera))
							{
								cam = tmp_text_ugui_2.canvas.worldCamera;
							}
							else
							{
								cam = Camera.main;
							}
						}

						second_link_index = TMP_TextUtilities.FindIntersectingLink(tmp_text_ugui_2, Input.mousePosition, cam);
						if (second_link_index != -1)
						{
							//2 - Выполнять необходимые действия - Сначала анализ ссылок, а затем Создание заметок и т.д.

							string link_one_text = tmp_text_ugui_2.textInfo.linkInfo[first_link_index].GetLinkText();
							string link_two_text = tmp_text_ugui_2.textInfo.linkInfo[second_link_index].GetLinkText();

							Debug.Log(link_one_text + " + " + link_two_text);

							LinkProcess.CheckLinks(tmp_text_ugui_2.textInfo.linkInfo[first_link_index].GetLinkID(), 
								tmp_text_ugui_2.textInfo.linkInfo[second_link_index].GetLinkID());
						}
					}
				}
			}
	}
}