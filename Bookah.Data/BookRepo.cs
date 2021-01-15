using Bookah.Data.Interfaces;
using Bookah.Data.Utilities;
using Bookah.Models;
using Firebase.Storage;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bookah.Data
{
    public class BookRepo : IBookRepo
    {
       

        public async Task Create(Book book)
        {
            
            AppHelper.client = new FireSharp.FirebaseClient(AppHelper.config);
            var data = book;
            FirebaseResponse response = await AppHelper.client.PushAsync("Book/", data);
            //SetResponse setResponse = await client.PushAsync("Book/" + data.BookID, data);
            //var result = await client.SetAsync("Book/" +book.BookID,book);
        }

        public async void Upload(FileStream stream, string file)
        {

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
               AppHelper.Bucket,
                new FirebaseStorageOptions
                {
                    //AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                //.Child("BookImages/")
                .Child(file)
                .PutAsync(stream, cancellation.Token);
            try
            {
                //error during upload will be thrown when you await the task
                string link = await task;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
