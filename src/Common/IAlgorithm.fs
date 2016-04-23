namespace Common

open GraphX
open GraphX.PCL.Common.Models
open Mono.Addins
open QuickGraph

[<TypeExtensionPoint>]
[<Interface>]
type IAlgorithm =
    abstract member Name : string
    abstract member Author : string
    abstract member Description : string
    abstract member Options : System.Windows.Forms.Panel
    abstract member Output : System.Windows.Forms.Panel
    abstract member Run : string -> unit
