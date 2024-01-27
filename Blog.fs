module Blog 

open System
open System.IO
open System.Threading.Tasks
open Microsoft.AspNetCore.Http
open Giraffe

[<CLIMutable>]
//--------------------------------------------------------------------------------------//
//                                      Main code                                       //
//--------------------------------------------------------------------------------------//

type LoginInfo = {
    username: string
    password: string
}

let username = "Michael"
let password = "Chung"

let loginHttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let! data = ctx.BindJsonAsync<LoginInfo>()
            if data.username = "Michael" && data.password = "Chung" 
            then
                ctx.SetStatusCode 200
                return! (json ("Logged In") ) next ctx
            else
                ctx.SetStatusCode 400
                return! json ("Incorrect Username or Password") next ctx // Task.FromResult None
        }

//--------------------------------------------------------------------------------------//
//                            Boilerplate code (IREELEVANT)                             //
//--------------------------------------------------------------------------------------//

(*

type BlogPost = {
    title: string
    content: string
}

type BlogDb() =

    let mutable allBlogPosts : BlogPost list = []

    member this.GetAllPosts = fun() -> allBlogPosts 

    member this.AddPost (newPost : BlogPost) =
        allBlogPosts <- (newPost::allBlogPosts)
        allBlogPosts

type BlogServiceTree = {
    getBlogDb : unit -> BlogDb
}

let getPostsHttpHandler (serviceTree: BlogServiceTree) =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        printfn "HELLOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO"
        json (serviceTree.getBlogDb().GetAllPosts()) next ctx

let createPostHttpHandler (serviceTree: BlogServiceTree) =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let! newPostJson = ctx.BindJsonAsync<BlogPost>()
            serviceTree.getBlogDb().AddPost(newPostJson) |> ignore
            return! json (newPostJson) next ctx
        }
        // Giraffe uses the native .NET "Task" type which are objects that replace the historic F# async {} workflows.
        // It is useful as it removes the necessity of converting between tasks and async workflows (because ASP.NET Core
        // can only work with tasks)
*)