using System;
using System.Collections.Generic;

namespace edocle.external.tools
{
    public class AsyncProcessTask
    {
        public string Name { get; private set; }
        public ProcessTaskState State { get; private set; }
        List<AsyncProcessTask> m_SubTasks;

        // calls
        public AsyncProcessTask(string name, Action<AsyncProcessTask> onCompletedSuccess, Action<AsyncProcessTask> onCompletedFail, float timeOutValue = 0)
        {
            Name = name;

            OnCompletedSuccess = onCompletedSuccess;
            OnCompletedFail = onCompletedFail;

            State = ProcessTaskState.inProgress;
        }
        public void CompleteProcess(ProcessTaskState state = ProcessTaskState.done_success)
        {
            if (State != ProcessTaskState.inProgress)
                return; // already completed

            if (state == ProcessTaskState.inProgress)
                return; // has no sense

            switch (state)
            {
                case ProcessTaskState.done_success:
                case ProcessTaskState.done_success_waitingSubTasks: // Not supposed to receive it from external sources but who cares
                    TryCompleteTask(state);
                    break;
                default:
                case ProcessTaskState.done_fail_timeout:
                case ProcessTaskState.done_fail_unknown:
                case ProcessTaskState.done_fail_subtaskFailed:
                    CancelTask(state);
                    break;
            }
        }

        public bool IsFailed()
        {
            return State == ProcessTaskState.done_fail_subtaskFailed ||
                State == ProcessTaskState.done_fail_timeout ||
                State == ProcessTaskState.done_fail_unknown;
        }

        public bool IsSuccess()
        {
            return State == ProcessTaskState.done_success;
        }

        // actions
        Action<AsyncProcessTask> OnCompletedSuccess;
        Action<AsyncProcessTask> OnCompletedFail;
        Action<AsyncProcessTask> OnSubTaskCompleted;

        // implementation

        void TryCompleteTask(ProcessTaskState state)
        {
            if (m_SubTasks != null &&  m_SubTasks.Count > 0)
            {
                foreach (var subTask in m_SubTasks)
                {
                    if (subTask.IsFailed())
                    {
                        CancelTask(ProcessTaskState.done_fail_subtaskFailed);
                        return;
                    }

                    if (!subTask.IsSuccess())
                    {
                        State = ProcessTaskState.done_success_waitingSubTasks;
                        return;
                    }
                }
            }

            CompleteTask();
        }

        void CompleteTask()
        {
            State = ProcessTaskState.done_success;
            OnCompletedSuccess?.Invoke(this);
        }

        void CancelTask(ProcessTaskState state)
        {
            State = state;
            CancelAllSubtasks();
            OnCompletedFail?.Invoke(this);
        }

        void CancelAllSubtasks()
        {
            if (m_SubTasks != null)
            {
                foreach (var subTask in m_SubTasks)
                {
                    if (subTask.State == ProcessTaskState.inProgress)
                    {
                        subTask.CompleteProcess(ProcessTaskState.done_fail_unknown);
                    }
                }
            }
        }
    }



    public enum ProcessTaskState
    {
        inProgress,
        done_success,
        done_success_waitingSubTasks,
        done_fail_subtaskFailed,
        done_fail_timeout,
        done_fail_unknown,
    }
}