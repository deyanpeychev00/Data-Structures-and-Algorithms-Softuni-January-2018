using System;
using System.Collections.Generic;

public class Judge : IJudge
{
    List<int> usersById, contestsById;
    List<Submission> submissions;

    bool usersSorted, contestsSorted, submissionsSorted;

    public Judge()
    {
        this.usersById = this.contestsById = new List<int>();
        this.submissions = new List<Submission>();
        usersSorted = contestsSorted = submissionsSorted = false;
    }

    public void AddContest(int contestId)
    {
        if (!this.contestsById.Contains(contestId))
        {
            this.contestsById.Add(contestId);
        }
    }

    public void AddSubmission(Submission submission)
    {
        if(this.usersById.Contains(submission.UserId) || this.contestsById.Contains(submission.ContestId))
        {
            throw new InvalidOperationException();
        }
        this.submissions.Add(submission);
    }

    public void AddUser(int userId)
    {
        if (!this.usersById.Contains(userId))
        {
            this.usersById.Add(userId);
        }
    }

    public void DeleteSubmission(int submissionId)
    {
        this.submissions.RemoveAll(x => x.Id == submissionId);
    }

    public IEnumerable<Submission> GetSubmissions()
    {
        if (!submissionsSorted)
        {
            this.submissions.Sort((a, b) => a.Id - b.Id);
            submissionsSorted = true;
        }
        foreach (var submission in this.submissions)
        {
            yield return submission;
        }
    }

    public IEnumerable<int> GetUsers()
    {
        if (!usersSorted)
        {
            this.usersById.Sort();
            usersSorted = true;
        }
      
        foreach (var userId in this.usersById)
        {
            yield return userId;
        }
    }

    public IEnumerable<int> GetContests()
    {
        if (!contestsSorted)
        {
            this.contestsById.Sort();
            contestsSorted = true;
        }

        foreach (var contestId in this.contestsById)
        {
            yield return contestId;
        }
    }

    public IEnumerable<Submission> SubmissionsWithPointsInRangeBySubmissionType(int minPoints, int maxPoints, SubmissionType submissionType)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<int> ContestsByUserIdOrderedByPointsDescThenBySubmissionId(int userId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Submission> SubmissionsInContestIdByUserIdWithPoints(int points, int contestId, int userId)
    {
        var result = this.submissions.FindAll(x => CheckCriterias(x, points, contestId, userId)).ToArray();
        foreach (var element in result)
        {
            yield return element;
        }
    }

    private bool CheckCriterias(Submission x, int points, int contestId, int userId)
    {
        return x.UserId == userId && x.Points == points && x.ContestId == contestId;
    }

    public IEnumerable<int> ContestsBySubmissionType(SubmissionType submissionType)
    {
        var result = this.submissions.FindAll(x => x.Type.CompareTo(submissionType) == 0).ToArray();
        foreach (var element in result)
        {
            yield return element.Id;
        }
    }
}
