namespace Common

open Mono.Addins

[<assembly: AddinRoot("GraphTasks", "1.0")>]
do()

[<TypeExtensionPoint>]
[<Interface>]
type IAlgorithm =
    abstract member Name : string
    abstract member Author : string
    abstract member Description : string

    abstract member Options : System.Windows.Forms.Panel
    abstract member Output : System.Windows.Forms.Panel

    abstract member Run : string -> unit
    abstract member NextStep : unit -> unit
    abstract member PreviousStep : unit -> unit

    abstract member CanGoFurther : bool
    abstract member CanGoBack : bool