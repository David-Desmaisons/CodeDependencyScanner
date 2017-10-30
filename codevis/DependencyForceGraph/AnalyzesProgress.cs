// This code is distributed under MIT license. 
// Copyright (c) 2014 George Mamaladze, Florian Greinacher
// See license.txt or http://opensource.org/licenses/mit-license.php

#region usings

using System;

#endregion

namespace Gma.CodeVisuals.Generator.DependencyForceGraph
{
    public class AnalyzesProgress : EventArgs
    {
        private readonly int m_Actual;
        private readonly int m_Max;
        private readonly bool m_Finished;
        private readonly string m_Message;

        public static AnalyzesProgress Started()
        {
            return new AnalyzesProgress("Started", 0, 1, false);
        }

        public static AnalyzesProgress Finished()
        {
            return new AnalyzesProgress("Finished", 0, 1, true);
        }

        public static AnalyzesProgress Idle()
        {
            return new AnalyzesProgress("Idle", 0, 1, false);
        }

        public AnalyzesProgress(string message, int actual, int max, bool finished)
        {
            m_Message = message;
            m_Actual = actual;
            m_Max = max;
            m_Finished = finished;
        }

        public string Message
        {
            get { return m_Message; }
        }

        public int Actual
        {
            get { return m_Actual; }
        }

        public int Max
        {
            get { return m_Max; }
        }

        public bool IsFinished
        {
            get { return m_Finished; }
        }
    }
}