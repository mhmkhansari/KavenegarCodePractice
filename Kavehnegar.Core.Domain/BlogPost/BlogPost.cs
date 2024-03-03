using Kavehnegar.Core.Domain.User;
using Kavehnegar.Shared.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Domain.BlogPost
{
    public class BlogPost : AggregateRoot<BlogPostId>
    {
        public BlogPostTitle Title { get; private set; }
        public BlogPostDescription? Description { get; private set; }
        public UserId authorId { get; private set; }
        public BlogPostState State { get; private set; }

        //Used to navigate to users table through corresponding foreign key
        public virtual User.User Author { get; private set; }

        //Created to have no headaches in EF migrations
        protected BlogPost() { }
        public BlogPost(BlogPostId id, BlogPostTitle title, UserId _authorId)
        {
            //Raise event since object creation alters domain state
            Apply(new Events.BlogPostCreated
            {
                Id = id.Value,
                AuthorId = _authorId.Value,
            });

            Id = id;
            Title = title;
            authorId = _authorId;
        }
        public void Update(BlogPostTitle title)
        {
            Apply(new Events.BlogPostUpdated
            {
                Id = Id.Value,
               
            });
            Title = title;
        }
        public void SetDescription(BlogPostDescription description)
        {
            Apply(new Events.DescriptionChanged()
            {
                OldValue = this?.Description ?? "",
                NewValue = description
            });
            Description = description;
        }
        public void Publish() => State = BlogPostState.Published;
        protected override void EnsureValidState()
        {
            //TODO : Implement validating domain state consistency upon update
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.BlogPostCreated e:
                    State = BlogPostState.Created;
                    break;
                case Events.BlogPostUpdated e:
                    State = BlogPostState.Updated;
                    break;
            }
        }
    }

    public enum BlogPostState
    {
        Created = 1,
        Published = 2,
        Updated = 3,
        DescriptionChanged = 4
    }
}
