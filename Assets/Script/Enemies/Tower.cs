using System;
using UnityEngine;

namespace Script.Enemies
{
    public class Tower: Base
    {
        protected override void Start()
        {
            base.Start();
            type = Type.Tower;
        }
    }
}