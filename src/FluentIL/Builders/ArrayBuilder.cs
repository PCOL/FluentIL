namespace FluentIL.Builders
{
    using System;
    using System.Reflection.Emit;

    /// <summary>
    /// Generates the IL for handling an array.
    /// </summary>
    public class ArrayBuilder
    {
        /// <summary>
        /// The emitter to use.
        /// </summary>
        private readonly IEmitter emitter;

        /// <summary>
        /// A local varaible to hold the array.
        /// </summary>
        private ILocal localArray;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayBuilder"/> class.
        /// </summary>
        /// <param name="emitter">The IL generator.</param>
        /// <param name="arrayType">The type of array.</param>
        /// <param name="length">The length of the array.</param>
        /// <param name="localArray">Optional local variable.</param>
        public ArrayBuilder(IEmitter emitter, Type arrayType, int length, ILocal localArray = null)
        {
            if (localArray != null &&
                localArray.LocalType.IsArray == false)
            {
                throw new InvalidProgramException("The local array type is not an array");
            }
            
            this.emitter = emitter;
            this.localArray = localArray;
            if (this.localArray == null)
            {
                this.emitter.DeclareLocal(arrayType.MakeArrayType(), out this.localArray);
            }

            // Create the argument types array.
            this.emitter
                .LdcI4(length)
                .NewArr(arrayType)
                .StLoc(this.localArray);
        }

        /// <summary>
        /// Emits the IL to start a set operation for the given index.
        /// </summary>
        /// <param name="index">The array index to set.</param>
        public void SetStart(int index)
        {
            this.emitter
                .LdLoc(this.localArray)
                .LdcI4(index);
        }

        /// <summary>
        /// Emits the IL to store the element.
        /// </summary>
        public void SetEnd()
        {
            this.emitter.Emit(OpCodes.Stelem_Ref);
        }

        /// <summary>
        /// Emits the IL to set an element of an array.
        /// </summary>
        /// <param name="index">The index of the element to set.</param>
        /// <param name="action">The action to call to emit the set code.</param>
        public void Set(int index, Action action)
        {
            this.SetStart(index);

            if (action != null)
            {
                action();
            }

            this.SetEnd();
        }

        /// <summary>
        /// Emits the IL to set an element of an array.
        /// </summary>
        /// <param name="index">The index of the element to set.</param>
        /// <param name="action">The action to call to emit the set code.</param>
        public void Set(int index, Action<int> action)
        {
            this.SetStart(index);

            if (action != null)
            {
                action(index);
            }

            this.SetEnd();
        }

        /// <summary>
        /// Emits the IL to load the given array element onto the evaluation stack.
        /// </summary>
        /// <param name="index">The index of the element to load.</param>
        public void Get(int index)
        {
            this.emitter
                .LdLoc(this.localArray)
                .LdcI4(index)
                .Emit(OpCodes.Ldelem_Ref);
        }

        /// <summary>
        /// Emits the IL to load the array onto the evaluation stack.
        /// </summary>
        public void Load()
        {
            this.emitter.LdLoc(this.localArray);
        }
    }
}
