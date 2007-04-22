function toggle(toggleId, divId) 
{
    var tog=document.getElementById(toggleId);
    var div=document.getElementById(divId); 

    if (tog.innerText == "[-]")
    {
       div.style.display = "none";
       tog.innerText = "[+]";
    }
    else
    {
       div.style.display= "block";  
       tog.innerText = "[-]";
    }
}

function copyToClipboad(id)
{
   var el = document.getElementById(id);
   window.clipboardData.setData(
       "Text",
       el.innerText
       );
}

function mark(id)
{
   var element = document.getElementById(id);
   
   if (element.oldBackgroundColor == null)
   {
       element.oldBackgroundColor = element.style.backgroundColor;
       element.style.backgroundColor = "#F0F0F0";
   }
   else
   {
       element.style.backgroundColor = element.oldBackgroundColor;
       element.style.oldBackgroundColor = null;
   }
}

var timerId = null;
var toolTipId = null;

function showToolTip()
{
  toolTipId = this.tooltip;
  document.onmousemove = checkEl;
  checkToolTip();
}

function hideToolTip()
{
  var whichEl = document.all[toolTipId].style;
  whichEl.visibility = "hidden";
  toolTipId = null;
  
  if (timerId) clearTimeout(timerId);
  document.onmousemove = null;
}

function checkToolTip() 
{
  if (timerId) clearTimeout(timerID);
  var left = event.clientX + document.body.scrollLeft;
  var top = event.clientY + document.body.scrollTop + 20;
  timerId = setTimeout("displayToolTip(" + left + ", " + top + ")", 300);
}

function displayToolTip(left, top) 
{
  document.onmousemove = null;
  var whichEl = document.all[active].style;
  whichEl.left = left;
  whichEl.top = top;
  whichEl.visibility = "visible";
}