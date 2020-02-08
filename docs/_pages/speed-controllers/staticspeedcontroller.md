---
title: Static Speed Controller
permalink: /speed-controllers/staticspeedcontroller
---

Sets the speed to a constant value

## Format

~~~
{
    "Type": "StaticSpeedController",
    "Config": {
        "Speed": <int>
    }
}
~~~

## Variables

### Speed
<div class="variable-block" markdown="block">

Constant speed value from `0` to `100` in percent

**Note:** `0` value can be used to stop the device
{: .notice--info}

**Note:** Any value beetween `1` and `19` will be raised to `20` as this is the minimum operating speed
{: .notice--info}

**Required:** No<br>
**Default value:**
~~~
50
~~~
**Example:**
~~~
"Speed": 75
~~~

</div>

## Example

~~~
{
    "Type": "StaticSpeedController",
    "Config": {
        "Speed": 75
    }
}
~~~