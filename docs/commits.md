# Commits


## 08-08-2021
- ITab interface
- class name refactor

## 07-08-2021
- AppController: link with managed controllers on Awake (CameraController, UI Tabs, ...)
- AppController.Start: load config and catalog, update Tab Catalog UI and set camera start position 
- AppController: OnFocusEvent, OnInfoEvent and OnItemSelectEvent events
- AppController: SetCameraController, SetItemsTab, SetItemInfo methods
- AppController: UpdateItemInfo and LoadObject methods
- CameraController: methods initAppEvents, initInputMouseEvents, initInputTouchEvents
- CameraController: avoid movement if pointer is on canvas UI elements
- CameraController: setStartPosition method
- CameraController: FocusItemObject method
- ItemData: ScriptableObject for storing item properties
- ConfigReader: CatalogItem class 
- ConfigReader: readCatalog method
- FocusButtonUI
- InfoButtonUI
- ItemInfoUI
- ItemsTabUI
- ItemUI
- CatalogItem
- UI: tab item info
- UI: tab catalog changes

## 04-08-2021
- CameraController: gestione eventi input dal mouse
- InputManager: revisione eventi mouse
- UI: grafica pannello items

## 02-08-2021
- InputSystem: interactions for multitouch
- InputManager: events for pinch to zoom
- CameraController: interaction with InputManager with events
- CameraController: setPointerDown, setPointerPosition, setZoomEnabled, setZoomValue
- CameraController: DetectPick
- ISelectable interface
- Item class
- JsonReader class
- ConfigReader class
- TestConfigReader
- CursorController class
- build: settings for android build
- debug: settings for android debug

## 01-08-2021
- first commit
