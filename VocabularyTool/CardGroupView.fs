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

let newModel =
    { name = ""; description = ""; colors = colorList; SelectedColor = None; cultures = cultureList; CultureSelected = None;}


// Init
let init () =
    newModel, Cmd.none


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
        backgroundColor = Color.Coral,
        content = 
            View.Grid(
                rowdefs=[40.; GridLength.Star],
                coldefs=[30.; GridLength.Star; 30.],
                margin = Thickness(10.,10.,10.,10.),
                children = [
                    // Title
                    View.Label(text="NUEVO GRUPO DE CARTAS",
                        style = Device.Styles.TitleStyle,
                        horizontalOptions = LayoutOptions.Center,
                        verticalOptions = LayoutOptions.Center).GridRow(0).GridColumn(1);
                    // Close button
                    View.ImageButton(backgroundColor=Color.Transparent,
                        source="close2",
                        horizontalOptions = LayoutOptions.Center,
                        margin = Thickness(0.,0.,0.,0.)).GridRow(0).GridColumn(2);
                    // Card
                    View.Frame(
                        backgroundColor = Color.White,
                        cornerRadius=15.,
                        padding = Thickness(0.,0.,0.,0.),
                        content=View.Grid(
                            rowdefs = [GridLength.Auto; GridLength.Auto; GridLength.Auto; GridLength.Auto; GridLength.Star],
                            coldefs=[20.; GridLength.Star;],
                            rowSpacing = 25.,
                            verticalOptions = LayoutOptions.FillAndExpand,
                            children = [
                                // Color box view
                                View.BoxView(
                                    margin = Thickness(0.,0.,0.,0.),
                                    widthRequest = 20.,
                                    cornerRadius= CornerRadius(15.,0.,15.,0.),
                                    backgroundColor=
                                        match model.SelectedColor with
                                        | Some colorModel -> Color.getColor colorModel.color
                                        | _ -> Xamarin.Forms.Color.Transparent
                                ).GridColumn(0).GridRowSpan(5);
                                // Card Title
                                View.Editor(placeholder="Título",
                                    margin=Thickness(0.,20.,10.,0.)).GridRow(0).GridColumn(1);
                                // Card Description
                                View.Editor(placeholder="Descripción",
                                    heightRequest=100.,
                                    margin=Thickness(0.,10.,10.,0.)).GridRow(1).GridColumn(1);
                                // Cultures
                                View.StackLayout(horizontalOptions = LayoutOptions.Center,
                                    verticalOptions = LayoutOptions.Center,
                                    children = [
                                        CultureList.view model.cultures (CultureSelectedMsg >> dispatch)
                                    ]).GridRow(2).GridColumnSpan(2);
                                // Color selection
                                View.StackLayout(horizontalOptions = LayoutOptions.Center,
                                    verticalOptions = LayoutOptions.Center,
                                    children = [
                                        ColorList.view model.colors (ColorSelectedMsg >> dispatch)
                                    ]).GridRow(3).GridColumnSpan(2);
                                // Save & Cancel buttons
                                View.Grid(
                                    margin = Thickness(0.,0.,0.,20.),
                                    coldefs=[GridLength.Star; GridLength.Star],
                                    horizontalOptions = LayoutOptions.FillAndExpand,
                                    verticalOptions = LayoutOptions.End,
                                    children = [
                                        View.Button(text="CANCELAR", 
                                            style = Device.Styles.TitleStyle, 
                                            textColor = Xamarin.Forms.Color.Red,
                                            horizontalOptions = LayoutOptions.Center).GridColumn(0);
                                        View.Button(text="GUARDAR", 
                                            style = Device.Styles.TitleStyle, 
                                            textColor = Xamarin.Forms.Color.Green,
                                            horizontalOptions = LayoutOptions.Center).GridColumn(1);
                                    ]).GridRow(4).GridColumnSpan(2)
                            ]
                        )
                    ).GridRow(1).GridColumnSpan(3)
                ]
            )
        )



