function solve() {
    class Post {
        constructor(title, content) {
            this.title = title;
            this.content = content;
        }

        toString() {
            return `Post: ${this.title}\n` + `Content: ${this.content}`;
        }
    }

    class SocialMediaPost extends Post {
        constructor(title, content, likes, dislikes) {
            super(title, content);
            this.comments = [];
            this.likes = likes;
            this.dislikes = dislikes;
        }

        toString() {
            let result = super.toString();
            result += `\nRating: ${this.likes - this.dislikes}`;
            if (this.comments.length > 0) {

                result += `\nComments:\n * `;
                result += this.comments.join('\n * ');
            }
            return result;
        }

        addComment(comment) {
            this.comments.push(comment);
        }
    }

    class BlogPost extends Post {
        constructor(title, content, views) {
            super(title, content);
            this.views = views;
        }

        view() {
            this.views++;
            return this;
        }

        toString() {
            let result = super.toString();
            result += `\nViews: ${this.views}`;
            return result;
        }
    }

    return { Post, SocialMediaPost, BlogPost };
}