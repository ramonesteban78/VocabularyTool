module CardGroupView

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms
open Color
open Culture

// Model
type Model = {
    name : string;
    description : string; 
    colors : Color.Model list;
    SelectedColor : Color.Model option
    cultures: Culture.Model list
    CultureSelected : Culture.Model option;
}

// Message
type Msg =
    | CardGroupSaved
    | ColorSelectedMsg of Color.Msg
    | CultureSelectedMsg of Culture.Msg


// Lists
let colorList = 
    [{color = Color.Color.Black; isSelected = false};
     {color = Color.Color.Blue; isSelected = false};
     {color = Color.Color.Green; isSelected = false};
     {color = Color.Color.Orange; isSelected = false};
     {color = Color.Color.Pink; isSelected = false};
     {color = Color.Color.Red; isSelected = false};
     {color = Color.Color.Yellow; isSelected = false}]

let cultureList = 
    [{name="es"; flag="spanish"; isSelected = false};
     {name="en"; flag="english"; isSelected = false}]


// Functions
let private selectColor (color:Color) list =
    list |> List.map (fun (x) -> if x.color = color then { x with isSelected = true } else x )

let private pickColor color list =
    list |> List.tryFind (fun (x) -> x.color = color)

let private cultureListSelected culture list =
    list |> List.map (fun (x) -> if x.flag = culture.flag then { x with isSelected = true } else x )


// Init
let init () =
    { name = ""; description = ""; colors = colorList; SelectedColor = None; cultures = cultureList; CultureSelected = None;}, Cmd.none


// Update
let update msg model =
    match msg with
    | CardGroupSaved -> model, Cmd.none
    | ColorSelectedMsg message -> 
        match message with
        | Color.Msg.ColorSelected color ->
            let selectedColor = colorList |> pickColor color
            let newList = colorList |> selectColor color
            { model with SelectedColor = selectedColor; colors = newList }, Cmd.none
    | CultureSelectedMsg cultureMessage ->
        match cultureMessage with
        | Culture.Msg.CultureSelected culture ->
            { model with 
                CultureSelected = Some culture; 
                cultures = cultureList |> cultureListSelected culture }, Cmd.none


// View
let view (model : Model) (dispatch:Msg -> unit) = 
    View.ContentPage(
        backgroundColor = Color.LightGray,
        content = 
            View.Frame(
                backgroundColor = Color.White,
                margin = Thickness(10.,10.,10.,10.),
                cornerRadius=10.,
                content=View.Grid(
                    rowdefs = [50.; GridLength.Auto; GridLength.Auto; GridLength.Auto; GridLength.Auto],
                    rowSpacing = 25.,
                    children = [
                        View.Label(text="Nuevo grupo de cartas",
                            style = Device.Styles.TitleStyle).GridRow(0);
                        View.Entry(placeholder="Título").GridRow(1);
                        View.Editor(placeholder="Descripción").GridRow(2);
                        View.StackLayout(horizontalOptions = LayoutOptions.Center,
                            verticalOptions = LayoutOptions.Center,
                            children = [
                                CultureList.view model.cultures (CultureSelectedMsg >> dispatch)
                            ]).GridRow(3);
                        View.StackLayout(horizontalOptions = LayoutOptions.Center,
                            verticalOptions = LayoutOptions.Center,
                            children = [
                                ColorList.view model.colors (ColorSelectedMsg >> dispatch)
                            ]).GridRow(4);
                    ]
                )
            )
        )



