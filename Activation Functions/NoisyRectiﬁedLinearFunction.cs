﻿
// Accord Neural Net Library
// The Accord.NET Framework
// http://accord.googlecode.com
//
// Copyright © César Souza, 2009, 2010
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//


namespace Accord.Neuro.ActivationFunctions
{
    using System;
    using AForge;
    using AForge.Neuro;
    using AForge.Math.Random;
    /// <summary>
    ///   Gaussian stochastic activation function.
    /// </summary>
    /// 
    /// <remarks>
    ///   This function assumes output variables have been normalized
    ///   to have zero mean and unit variance.
    /// </remarks>
    ///
    [Serializable]
    public class NoisyRectiﬁedLinearFunction : IStochasticFunction
    {
        private double alpha; // sigmoid's alpha value
        private static Random random = new ThreadSafeRandom();
        private static GaussianGenerator gaussian = new GaussianGenerator(0, 1, Environment.TickCount);
        /// <summary>
        ///   Gets or sets the random sample generator
        ///   used to activate neurons of this class.
        /// </summary>
        /// 
        public static Random Random
        {
            get { return random; }
            set { random = value; }
        }

        /// <summary>
        ///   Sigmoid's alpha value.
        /// </summary>
        /// 
        /// <remarks><para>The value determines steepness of the function. Increasing value of
        /// this property changes sigmoid to look more like a threshold function. Decreasing
        /// value of this property makes sigmoid to be very smooth (slowly growing from its
        /// minimum value to its maximum value).</para>
        ///
        /// <para>Default value is set to <b>1</b>.</para>
        /// </remarks>
        /// 
        public double Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="BernoulliFunction"/> class.
        /// </summary>
        /// 
        /// <param name="alpha">Sigmoid's alpha value. Default is 1.</param>
        /// 
        public NoisyRectiﬁedLinearFunction(double alpha)
        {
            this.alpha = alpha;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="BernoulliFunction"/> class.
        /// </summary>
        /// 
        public NoisyRectiﬁedLinearFunction()
            : this(alpha: 1) { }


        /// <summary>
        /// Calculates function value.
        /// </summary>
        ///
        /// <param name="x">Function input value.</param>
        /// 
        /// <returns>Function output value, <i>f(x)</i>.</returns>
        ///
        /// <remarks>The method calculates function value at point <paramref name="x"/>.</remarks>
        ///
        public double Function(double x)
        {
            return (1 / (1 + Math.Exp(-alpha * x)));
        }

        /// <summary>
        ///   Samples a value from the function given a input value.
        /// </summary>
        /// 
        /// <param name="x">Function input value.</param>
        /// 
        /// <returns>Draws a random value from the function.</returns>
        /// 
        public double Generate(double x)
        {
            double y = Function(x);

            //return y + random.Next() > 0 ? y + random.Next() : 0;
            return y + gaussian.Next() > 0 ? y + gaussian.Next() : 0;
        }



        /// <summary>
        ///   Samples a value from the function given a function output value.
        /// </summary>
        /// 
        /// <param name="y">The function output value. This is the value which was obtained
        /// with the help of the <see cref="IActivationFunction.Function(double)"/> method.</param>
        /// 
        /// <remarks><para>The method calculates the same output value as the
        /// <see cref="Generate"/> method, but it takes not the input <b>x</b> value
        /// itself, but the function value, which was calculated previously with help
        /// of the <see cref="IActivationFunction.Function(double)"/> method.</para>
        /// </remarks>
        /// 
        /// <returns>Draws a random value from the function.</returns>
        /// 
        public double Generate2(double y)
        {
            //return y + random.Next() > 0 ? y + random.Next() : 0;
            return y + gaussian.Next() > 0 ? y + gaussian.Next() : 0;
            
        }

        /// <summary>
        /// Calculates function derivative.
        /// </summary>
        /// 
        /// <param name="x">Function input value.</param>
        /// 
        /// <returns>Function derivative, <i>f'(x)</i>.</returns>
        /// 
        /// <remarks>The method calculates function derivative at point <paramref name="x"/>.</remarks>
        ///
        public double Derivative(double x)
        {
            double y = Function(x);

            return (alpha * y * (1 - y));
        }

        /// <summary>
        /// Calculates function derivative.
        /// </summary>
        /// 
        /// <param name="y">Function output value - the value, which was obtained
        /// with the help of <see cref="Function"/> method.</param>
        /// 
        /// <returns>Function derivative, <i>f'(x)</i>.</returns>
        /// 
        /// <remarks><para>The method calculates the same derivative value as the
        /// <see cref="Derivative"/> method, but it takes not the input <b>x</b> value
        /// itself, but the function value, which was calculated previously with
        /// the help of <see cref="Function"/> method.</para>
        /// 
        /// <para><note>Some applications require as function value, as derivative value,
        /// so they can save the amount of calculations using this method to calculate derivative.</note></para>
        /// </remarks>
        /// 
        public double Derivative2(double y)
        {
            return (alpha * y * (1 - y));
        }

    }
}
