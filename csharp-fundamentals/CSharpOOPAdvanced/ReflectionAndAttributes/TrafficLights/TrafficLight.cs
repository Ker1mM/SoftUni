using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficLights
{
    public class TrafficLight
    {
        private LightColor color;

        public TrafficLight(LightColor color)
        {
            this.color = color;
        }

        public void ChangeColor()
        {
            this.color = this.GetNext(this.color);
        }

        public override string ToString()
        {
            return this.color.ToString();
        }

        private LightColor GetNext(LightColor color)
        {
            LightColor result = LightColor.Red;
            if (color == LightColor.Red)
            {
                result = LightColor.Green;
            }
            else if (color == LightColor.Green)
            {
                result = LightColor.Yellow;
            }

            return result;
        }
    }
}
