using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using BlogWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using BlogWebApp.Models;
using System.Security.Claims;
using System.Reflection.Metadata;
using System.Threading;

namespace BlogWebApp.Controllers
{
    public class BlogPostImageController : Controller
    {

        private readonly ILogger<BlogController> _logger;
        private readonly IBlogCosmosDbService _blogDbService;
        private readonly ImageStorageManager _imageStorageManager;

        public BlogPostImageController(ILogger<BlogController> logger, IBlogCosmosDbService blogDbService, ImageStorageManager imageStorageManager)
        {
            _logger = logger;
            _blogDbService = blogDbService;
            _imageStorageManager = imageStorageManager;
        }

        [Route("img/post/{postId}/{filename}")]
        public async Task<IActionResult> PostView(string postId, string filename, CancellationToken ct)
        {
            var blobName = $"{postId}/{filename}";

            //var imageBytes = await _imageStorageManager.GetBlobAsByteArray("blog-post-images", blobName);
            //return File(imageBytes, "image/png");
            
            var blob = await _imageStorageManager.GetBlobAsStream("blog-post-images", blobName, ct);
            return File(blob.Value.Content, blob.Value.Details.ContentType);

        }



    }
}