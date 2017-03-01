// <copyright file="MoveDirection.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2008 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-03-19</date>
// <summary>Возможные направления перемещения и масштабирования.</summary>

using System;
using System.Collections.Generic;

namespace Makarov.FlowchartBuilder
{
    /// <summary>
    /// Возможные направления перемещения и масштабирования.
    /// </summary>
    [Flags]
    public enum MoveDirection : byte
    {
        /// <summary>
        /// Движение запрещено.
        /// </summary>
        None = 0,

        /// <summary>
        /// Движение разрешено во все стороны.
        /// </summary>
        Move = 1,

        /// <summary>
        /// Вверх.
        /// </summary>
        Up = 2,

        /// <summary>
        /// Вниз.
        /// </summary>
        Down = 4,

        /// <summary>
        /// Влево.
        /// </summary>
        Left = 8,

        /// <summary>
        /// Вправо.
        /// </summary>
        Right = 16,

        /// <summary>
        /// Вверх и влево.
        /// </summary>
        UpAndLeft = 10,

        /// <summary>
        /// Вверх и вправо.
        /// </summary>
        UpAndRight = 18,

        /// <summary>
        /// Вниз и влево.
        /// </summary>
        DownAndLeft = 12,

        /// <summary>
        /// Вниз и вправо.
        /// </summary>
        DownAndRight = 20,

        /// <summary>
        /// Вращение.
        /// </summary>
        Rotate = 32
    }

    public static class MoveDirectionHelper
    {
        /// <summary>
        /// Вычисляет угол между направлениями движения.
        /// </summary>
        public static int CalculateAngle(this MoveDirection dir1, MoveDirection dir2)
        {
            var angles = new Dictionary<MoveDirection, int>
                             {
                                 {MoveDirection.Up, 0},
                                 {MoveDirection.UpAndRight, 45},
                                 {MoveDirection.Right, 90},
                                 {MoveDirection.DownAndRight, 135},
                                 {MoveDirection.Down, 180},
                                 {MoveDirection.DownAndLeft, 225},
                                 {MoveDirection.Left, 270},
                                 {MoveDirection.UpAndLeft, 315},
                             };

            return angles[dir1] - angles[dir2];
        }

        /// <summary>
        /// Поворачивает направление на угол между двумя другими направлениями.
        /// </summary>
        public static MoveDirection RotateDirection(this MoveDirection source, MoveDirection dir1, MoveDirection dir2)
        {
            var directions = GetDirectionsArray();
            int angle = dir1.CalculateAngle(dir2) / 45;
            if (angle < 0) angle = directions.Length + angle;
            if (angle > directions.Length) angle %= directions.Length;

            int curr = 0;
            for (int i = 0; i < directions.Length; i++)
                if (directions[i] == source)
                {
                    curr = i;
                    break;
                }

            curr += angle;
            if (curr > directions.Length) curr %= directions.Length;
            return directions[curr];
        }

        /// <summary>
        /// Возвращает массив направлений, расположенных по часовой стрелке.
        /// </summary>
        public static MoveDirection[] GetDirectionsArray()
        {
            return new[]
                       {
                           MoveDirection.Up,
                           MoveDirection.UpAndRight,
                           MoveDirection.Right,
                           MoveDirection.DownAndRight,
                           MoveDirection.Down,
                           MoveDirection.DownAndLeft,
                           MoveDirection.Left,
                           MoveDirection.UpAndLeft
                       };
        }
    }
}