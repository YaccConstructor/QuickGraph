namespace Common

open Mono.Addins
//open System.Windows.Controls

open GraphX
open GraphX.PCL.Common.Models
open QuickGraph

[<assembly:AddinRoot ("GraphTasks", "1.0")>]
do()

[<TypeExtensionPoint>]
[<Interface>]
type IAlgorithm = 
    abstract member Name : string
    abstract member Description : string
    abstract member Input : System.Windows.Forms.TextBox 
    abstract member Output : GraphX.Controls.GraphArea<VertexBase, EdgeBase<VertexBase>, BidirectionalGraph<VertexBase, EdgeBase<VertexBase>>> 


    
