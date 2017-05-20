﻿namespace OmniXaml.Services
{
    using ReworkPhases;

    public class NodeToObjectBuilder : INodeToObjectBuilder
    {
        private readonly ISmartInstanceCreator instanceCreator;
        private readonly IStringSourceValueConverter converter;
        private readonly IMemberAssigmentApplier memberAssigmentApplier;

        public NodeToObjectBuilder(ISmartInstanceCreator instanceCreator, IStringSourceValueConverter converter, IMemberAssigmentApplier memberAssigmentApplier)
        {
            this.instanceCreator = instanceCreator;
            this.converter = converter;
            this.memberAssigmentApplier = memberAssigmentApplier;
        }

        public object Build(ConstructionNode node)
        {
            var builder = new NodeAssembler(instanceCreator, converter, memberAssigmentApplier);

            builder.Assemble(node);
            builder.Assemble(node);
            return node.Instance;
        }
    }
}