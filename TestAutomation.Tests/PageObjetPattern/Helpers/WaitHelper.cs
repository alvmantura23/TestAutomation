using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TestAutomation.Tests.PageObjetPattern.Helpers
{
    public static class WaitHelper
    {
        public static void WaitForCondition(Func<bool> condition, int msTimeout = 4000)
        {
            // este codigo es muy util para controlar las esperas en los test, ya que permite esperar hasta que se cumpla una condicion, en este caso, la condicion es una funcion que devuelve un booleano, y el tiempo de espera es de 4 segundos por defecto.
            var stopWatch = new Stopwatch(); // Definimos una variable de tipo Stopwatch

            stopWatch.Start();

            Exception? ex;
            do
            {
                try
                {
                    ex = null;
                    if (condition())
                    {
                        return;
                    }
                }
                catch (Exception e)
                {
                    ex = e;
                }
            } while (stopWatch.ElapsedMilliseconds < msTimeout);

            stopWatch.Stop();

            if (ex != null)
            {
                throw new TimeoutException("Error executing the condition", ex);
            }
            throw new TimeoutException("Error the condition was false", ex); // si la condicion es falsa siempre
        }
    }
}
