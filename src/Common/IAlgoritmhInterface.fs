namespace Common

open GraphX
open GraphX.PCL.Common.Models
open Mono.Addins
open QuickGraph

[<TypeExtensionPoint>]
[<Interface>]
type IAlgorithm = 
    abstract member Name : string
    abstract member Description : string
    abstract member Author : string
    abstract member Input : System.Windows.Forms.TextBox 
    abstract member Output : GraphX.Controls.GraphArea<VertexBase, EdgeBase<VertexBase>, BidirectionalGraph<VertexBase, EdgeBase<VertexBase>>> 


    
