using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomSlider : MonoBehaviour
{
    private VisualElement _root;

    private List<Slider> _sliderList;
    private List<VisualElement> _draggerList;
    private List<VisualElement> _barList;
    private List<VisualElement> _newDraggerList;

    void OnEnable()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        _sliderList = new List<Slider>();
        _draggerList= new List<VisualElement>();
        _newDraggerList = new List<VisualElement>();
        _barList = new List<VisualElement>();


        _sliderList = _root.Query<Slider>("music").ToList();
        int size = _sliderList.Count;
        for(int i=0;i<size;++i)
        {
            AddElement(i);
            int index =i; 
            _sliderList[i].RegisterCallback<ChangeEvent<float>>(evt => OnSliderValueChanged(evt,_newDraggerList[index],_draggerList[index]));
            _sliderList[i].RegisterCallback<GeometryChangedEvent>(evt => SliderInit(evt,_newDraggerList[index],_draggerList[index]));
        }

    }

    private void AddElement(int i)
    {
        _draggerList.Add(_sliderList[i].Q<VisualElement>("unity-dragger"));

        VisualElement _bar = new VisualElement();
        _barList.Add(_bar);
        _draggerList[i].Add(_bar);
        _bar.name ="Bar";
        _bar.AddToClassList("bar");

        VisualElement _newDragger = new VisualElement();
        _newDraggerList.Add(_newDragger);
        _sliderList[i].Add(_newDragger);
        _newDragger.name = "NewDragger";
        _newDragger.AddToClassList("newDragger");
        _newDragger.pickingMode = PickingMode.Ignore;

    }

    private void OnSliderValueChanged(ChangeEvent<float> value,VisualElement newDragger, VisualElement dragger)
    {
        Vector2 dist = new Vector2((newDragger.layout.width - dragger.layout.width)/2,(newDragger.layout.height - dragger.layout.height)/2);
        Vector2 pos =dragger.parent.LocalToWorld(dragger.transform.position);
        newDragger.transform.position = newDragger.parent.WorldToLocal(pos-dist);
    }

    private void SliderInit(GeometryChangedEvent evt,VisualElement newDragger, VisualElement dragger)
    {
        Vector2 dist = new Vector2((newDragger.layout.width - dragger.layout.width)/2,(newDragger.layout.height - dragger.layout.height)/2);
        Vector2 pos =dragger.parent.LocalToWorld(dragger.transform.position);
        newDragger.transform.position = newDragger.parent.WorldToLocal(pos-dist);
    }
}
