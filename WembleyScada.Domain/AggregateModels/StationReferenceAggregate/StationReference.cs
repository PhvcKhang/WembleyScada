namespace WembleyScada.Domain.AggregateModels.StationReferenceAggregate;

public class StationReference : IAggregateRoot
{
    public string ReferenceId { get; private set; }
    public Reference Reference { get; private set; }
    public string StationId { get; private set; }
    public Station Station { get; private set; }
    public List<MFC> MFCs { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public StationReference() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public void UpdateMFCs(List<MFC> mFCs)
    {
        foreach (MFC mFC in mFCs)
        {
            var mFCToUpdate = MFCs.Find(x => x.MFCName == mFC.MFCName);

            if (mFCToUpdate != null)
            {
                MFCs.Remove(mFCToUpdate);
                mFCToUpdate.UpdateMFCValues(mFC);
                MFCs.Add(mFCToUpdate);
            }
        }
    }
}
