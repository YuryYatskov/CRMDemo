using CRMWeb.Extensions;
using CRMWeb.Models.Phones;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using System.Collections;

namespace CRMWeb.Services.PhoneNotes;

public class PhoneNoteAdaptor : DataAdaptor
{
    private readonly IPhoneNoteService _phoneNoteService;

    public PhoneNoteAdaptor()
    {
        _phoneNoteService = SelectiveServices.PhoneNoteService!;
    }

    public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string? key = null)
    {
        var pageSize = dataManagerRequest.Take == 0 ? int.MaxValue : dataManagerRequest.Take;
        var pageIndex = pageSize == 0 ? 0 : dataManagerRequest.Skip / pageSize; // pageSize / dataManagerRequest.Skip;

        var response = await _phoneNoteService.GetPhoneNotes(pageIndex, pageSize);

        if (response.IsSuccessStatusCode)
        {
            IEnumerable<PhoneNoteModel> dataSource = response.Content!.PhoneNotes.Data;

            // Handling Searching in CustomAdaptor.
            if (dataManagerRequest.Search != null && dataManagerRequest.Search.Count > 0)
            {
                // Searching
                dataSource = DataOperations.PerformSearching(dataSource, dataManagerRequest.Search);
            }

            // Handling Filtering in CustomAdaptor.
            if (dataManagerRequest.Where != null && dataManagerRequest.Where.Count > 0)
            {
                // Filtering
                dataSource = DataOperations.PerformFiltering(dataSource, dataManagerRequest.Where, dataManagerRequest.Where[0].Operator);
                //dataSource = DataOperations.PerformFiltering(dataSource, dataManagerRequest.Where, dataManagerRequest.Where[0].Condition);
            }

            // Handling Sorting in CustomAdaptor.
            if (dataManagerRequest.Sorted != null && dataManagerRequest.Sorted.Count > 0)
            {
                // Sorting
                dataSource = DataOperations.PerformSorting(dataSource, dataManagerRequest.Sorted);
            }

            var count = response.Content!.PhoneNotes.Count;

            // Handling Aggregates in CustomAdaptor.
            IDictionary<string, object>? aggregates = null;
            if (dataManagerRequest.Aggregates != null) // Aggregation
            {
                aggregates = DataUtil.PerformAggregation(dataSource, dataManagerRequest.Aggregates);
            }

            // Handling Paging in CustomAdaptor. For example, Skip is 0 and Take is equal to page size for first page.
            //if (dataManagerRequest.Skip != 0)
            //{
            //    //Paging
            //    dataSource = DataOperations.PerformSkip(dataSource, dataManagerRequest.Skip);
            //}

            //if (dataManagerRequest.Take != 0)
            //{
            //    dataSource = DataOperations.PerformTake(dataSource, dataManagerRequest.Take);
            //}

            // Handling Grouping in CustomAdaptor.
            DataResult dataObject = new DataResult();
            if (dataManagerRequest.Group != null)
            {
                IEnumerable resultData = dataSource.ToList();
                // Grouping
                foreach (var group in dataManagerRequest.Group)
                {
                    resultData = DataUtil.Group<PhoneNoteModel>(resultData, group, dataManagerRequest.Aggregates, 0, dataManagerRequest.GroupByFormatter);
                }
                dataObject.Result = resultData;
                dataObject.Count = (int)count;

                //If both Grouping and Aggregate is enabled
                if (dataManagerRequest.Aggregates != null)
                {
                    dataObject.Aggregates = aggregates;
                }

                //Here RequiresCount is passed from the control side itself, where ever the on-demand data fetching is needed then the RequiresCount is set as true in component side itself.
                // In the above case we are using paging so datas are loaded in on-demand bases whenever the next page is clicked in Blazor DataGrid side.
                return dataManagerRequest.RequiresCounts ? dataObject : resultData;
            }

            //Here RequiresCount is passed from the control side itself, where ever the on-demand data fetching is needed then the RequiresCount is set as true in component side itself.
            // In the above case we are using paging so datas are loaded in on-demand bases whenever the next page is clicked in Blazor DataGrid side.
            return dataManagerRequest.RequiresCounts ? new DataResult() { Result = dataSource, Count = (int)count, Aggregates = aggregates } : dataSource;
        }
        else
        {
            Console.WriteLine($"API Error (read): {response.StatusCode} - {response.Error?.Message} - {response.Error?.Content}");
            throw new Exception(response.Error?.Message, response.Error);
        }
    }

    public override async Task<object?> InsertAsync(DataManager dataManager, object data, string key)
    {
        var insert = (PhoneNoteModel)data;

        var response = await _phoneNoteService.CreatePhoneNote(new CreatePhoneNoteRequest(insert));

        if (response.IsSuccessStatusCode)
        {
            CreatePhoneNoteResponse createResponse = response.Content!;
            return createResponse;
        }
        else
        {
            Console.WriteLine($"API Error (insert): {response.StatusCode} - {response.Error?.Message} - {response.Error?.Content}");
            throw new Exception(response.Error?.Content, response.Error);
        }
    }

    public override async Task<object> UpdateAsync(DataManager dataManager, object data, string keyField, string key)
    {
        var response = await _phoneNoteService.UpdatePhoneNote(new UpdatePhoneNoteRequest((PhoneNoteModel)data));
        if (response.IsSuccessStatusCode)
        {
            UpdatePhoneNoteResponse updateResponse = response.Content!;
            return updateResponse;
        }
        else
        {
            Console.WriteLine($"API Error (insert): {response.StatusCode} - {response.Error?.Message} - {response.Error?.Content}");
            throw new Exception(response.Error?.Content, response.Error);
        }
    }

    public override async Task<object> RemoveAsync(DataManager dataManager, object data, string keyField, string key)
    {
        var response = await _phoneNoteService.DeletePhoneNote((Guid)data);

        if (response.IsSuccessStatusCode)
        {
            DeletePhoneNoteResponse deleteResponse = response.Content!;
            return deleteResponse;
        }
        else
        {
            Console.WriteLine($"API Error (insert): {response.StatusCode} - {response.Error?.Message} - {response.Error?.Content}");
            throw new Exception(response.Error?.Content, response.Error);
        }
    }
}

