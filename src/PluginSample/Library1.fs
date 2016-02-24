namespace PluginSample
open Mono.Addins
open Common
//open System.Windows.Controls
open GraphX
open GraphX.PCL.Common.Models
open QuickGraph
open System.Windows.Forms

[<assembly:Addin>]
[<assembly:AddinDependency ("GraphTasks", "1.0")>]
do()
[<Extension>]
type SamplePlugin() = 
    interface IAlgorithm with
        member this.Name = "SamplePlugin"
        member this.Description = "Sample"
        member this.Input = new System.Windows.Forms.TextBox()
        member this.Output = new GraphX.Controls.GraphArea<VertexBase, EdgeBase<VertexBase>, BidirectionalGraph<VertexBase, EdgeBase<VertexBase>>> ()
        

