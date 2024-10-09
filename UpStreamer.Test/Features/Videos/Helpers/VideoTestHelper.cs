using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpStreamer.Server.Database.Entities;

namespace UpStreamer.Test.Features.Videos.Helpers
{
    public static class VideoTestHelper
    {
        private static Category Category => new() { Name = "Test", Id = 1 };

        public static List<Video> GetVideos() {
            List<Video> list = [];
            for(var i = 0; i <= 30; i++){
                list.Add(new()
                {
                    Title = "Title",
                    Description = "Desc",
                    FilePath = "FilePath",
                    Category = Category,
                    CategoryId = 1
                });
            }

            return list;
        }
    }
}
