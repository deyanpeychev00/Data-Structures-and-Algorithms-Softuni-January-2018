using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Chainblock : IChainblock
{
    public int Count => this.transactionsById.Keys.Count;

    Dictionary<int, Transaction> transactionsById;

    public Chainblock()
    {
        transactionsById = new Dictionary<int, Transaction>();
    }

    public void Add(Transaction tx)
    {
        if (!this.transactionsById.ContainsKey(tx.Id))
        {
            transactionsById.Add(tx.Id, tx);
        }
    }

    public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
    {
        if (!this.transactionsById.ContainsKey(id))
        {
            throw new ArgumentException();
        }
        this.transactionsById[id].Status = newStatus;
    }

    public bool Contains(Transaction tx)
    {
        return this.transactionsById.ContainsKey(tx.Id);
    }

    public bool Contains(int id)
    {
        return this.transactionsById.ContainsKey(id);
    }

    public IEnumerable<Transaction> GetAllInAmountRange(double lo, double hi)
    {
        return this.transactionsById.Values.Where(x => x.Amount >= lo && x.Amount <= hi);
    }

    public IEnumerable<Transaction> GetAllOrderedByAmountDescendingThenById()
    {
        return this.transactionsById.Values.OrderByDescending(x => x.Amount).ThenBy(x => x.Id);
    }

    public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
    {
        List<Transaction> statuses = this.transactionsById.Values.Where(x => x.Status == status).OrderByDescending(x => x.Amount).ToList();
        if (statuses.Count == 0)
        {
            throw new InvalidOperationException();
        }
        return statuses.Select(x => x.To).ToList();
    }

    public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
    {
        List<Transaction> statuses = this.transactionsById.Values.Where(x => x.Status == status).OrderByDescending(x=> x.Amount).ToList();
        if (statuses.Count == 0)
        {
            throw new InvalidOperationException();
        }
        return statuses.Select(x => x.From).ToList();
    }

    public Transaction GetById(int id)
    {
        if (!this.transactionsById.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }
        return this.transactionsById[id];
    }

    public IEnumerable<Transaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
    {
        var result =  this.transactionsById.Values.Where(x => x.To == receiver && x.Amount >= lo && x.Amount < hi).OrderByDescending(x => x.Amount).ThenBy(x => x.Id);
        if(result.ToList().Count == 0)
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public IEnumerable<Transaction> GetByReceiverOrderedByAmountThenById(string receiver)
    {
        var result = this.transactionsById.Values.Where(x => x.To == receiver).OrderByDescending(x => x.Amount).ThenBy(x => x.Id);
        if(result.ToList().Count == 0)
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public IEnumerable<Transaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
    {
        var result = this.transactionsById.Values.Where(x => x.From == sender && x.Amount > amount).OrderByDescending(x => x.Amount);
        if(result.ToList().Count == 0)
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public IEnumerable<Transaction> GetBySenderOrderedByAmountDescending(string sender)
    {
        var result = this.transactionsById.Values.Where(x => x.From == sender).OrderByDescending(x => x.Amount);
        if (result.ToList().Count == 0)
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public IEnumerable<Transaction> GetByTransactionStatus(TransactionStatus status)
    {
        var result =  this.transactionsById.Values.Where(x => x.Status == status).OrderByDescending(x => x.Amount);
        if(result.ToList().Count == 0)
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public IEnumerable<Transaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
    {
        var result = this.transactionsById.Values.Where(x => x.Status == status && x.Amount <= amount).OrderByDescending(x => x.Amount);
        if (result.ToList().Count == 0)
        {
            return new List<Transaction>();
        }
        return result;
    }

    public IEnumerator<Transaction> GetEnumerator()
    {
        foreach (var item in this.transactionsById.Values)
        {
            yield return item;
        }
    }

    public void RemoveTransactionById(int id)
    {
        if (!this.transactionsById.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }
        this.transactionsById.Remove(id);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

