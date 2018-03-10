using System;
using System.Collections.Generic;
using System.Linq;

public class Judge : IJudge
{
    Dictionary<int, Submission> bySubmissionID = new Dictionary<int, Submission>();
    HashSet<int> users = new HashSet<int>();
    HashSet<int> contests = new HashSet<int>();

    public Judge()
    {

    }

    public void AddContest(int contestId)
    {
        this.contests.Add(contestId);
    }

    public void AddSubmission(Submission submission)
    {
        if (!this.bySubmissionID.ContainsKey(submission.Id))
        {
            return;
        }
        if(!this.users.Contains(submission.UserId) || !this.contests.Contains(submission.ContestId))
        {
            throw new InvalidOperationException();
        }
        this.bySubmissionID.Add(submission.Id, submission);
    }

    public void AddUser(int userId)
    {
        this.users.Add(userId);
    }

    public void DeleteSubmission(int submissionId)
    {
        // Submission submission = this.bySubmissionID[submissionId];
        if (!this.bySubmissionID.ContainsKey(submissionId))
        {
            throw new InvalidOperationException();
        }
        this.bySubmissionID.Remove(submissionId);
    }

    public IEnumerable<Submission> GetSubmissions()
    {
        return this.bySubmissionID.Values.OrderBy(x => x.Id);
    }

    public IEnumerable<int> GetUsers()
    {
        return this.users.OrderBy(x => x);
    }

    public IEnumerable<int> GetContests()
    {
        return this.contests.OrderBy(x => x);
    }

    public IEnumerable<Submission> SubmissionsWithPointsInRangeBySubmissionType(int minPoints, int maxPoints, SubmissionType submissionType)
    {
        return this.bySubmissionID.Values.Where(x => 
        x.Points >= minPoints && 
        x.Points <= maxPoints && 
        x.Type == submissionType);
    }

    public IEnumerable<int> ContestsByUserIdOrderedByPointsDescThenBySubmissionId(int userId)
    {
        return this.bySubmissionID.Values
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Points)
            .ThenBy(x => x.Id)
            .Select(x => x.ContestId)
            .Distinct();
    }

    public IEnumerable<Submission> SubmissionsInContestIdByUserIdWithPoints(int points, int contestId, int userId)
    {
        IEnumerable<Submission> result =  this.bySubmissionID.Values.Where(x => 
        x.UserId == userId && 
        x.Points == points && 
        x.ContestId == contestId);

        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result;
    }


    public IEnumerable<int> ContestsBySubmissionType(SubmissionType submissionType)
    {
        return this.bySubmissionID.Values
            .Where(x => x.Type == submissionType)
            .Select(x => x.ContestId)
            .Distinct();
    }
}
