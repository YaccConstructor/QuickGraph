namespace PluginSample

open Common
open GraphX
open GraphX.PCL.Common.Models
open Mono.Addins
open QuickGraph
open System.Windows.Forms

[<assembly:Addin>]
[<assembly:AddinDependency ("GraphTasks", "1.0")>]
do()

[<Extension>]
type SamplePlugin() = 
    interface IAlgorithm with
        member this.Name = "Sample Plugin"
        member this.Description = "This plugin demonstrates how to use plugin system.\n" +
                                  "Add references to your plugins to MainForm project.\n" +
                                  "Read more at Mono.Addins wiki page on GitHub."
        member this.Author = "Anastasiya Ragozina & Eugene Auduchinok"
        member this.Input = new System.Windows.Forms.TextBox()
        member this.Output = new GraphX.Controls.GraphArea<VertexBase, EdgeBase<VertexBase>, BidirectionalGraph<VertexBase, EdgeBase<VertexBase>>> ()
        

