export const TYPES = {
    Commands: {
        IAddSomethingHandler: Symbol.for("SampleServices.Commands.IAddSomethingHandler"),
        IUpdateSomethingHandler: Symbol.for("SampleServices.Commands.IUpdateSomethingHandler"),
        IRemoveSomethingHandler: Symbol.for("SampleServices.Commands.IRemoveSomethingHandler")
    },
    Queries: Symbol.for("SampleServices.Queries")
}